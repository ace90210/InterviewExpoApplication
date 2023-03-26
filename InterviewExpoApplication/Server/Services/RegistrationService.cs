using InterviewExpoApplication.Data.Repositories;
using InterviewExpoApplication.Shared.Company;
using InterviewExpoApplication.Shared.Configuration;
using InterviewExpoApplication.Shared.EventRegistration;
using InterviewExpoApplication.Shared.User;
using Microsoft.Extensions.Options;

namespace InterviewExpoApplication.Server.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationsRepository;
        private readonly HttpClient httpClient;
        private ApplicationConfiguration applicationConfiguration;
        private static Random rnd = new Random();

        public RegistrationService(IRegistrationRepository userRepository, IOptions<ApplicationConfiguration> applicationConfigurationOptions, HttpClient httpClient)
        {
            this.registrationsRepository = userRepository;
            this.httpClient = httpClient;
            this.applicationConfiguration = applicationConfigurationOptions.Value;
        }

        public async Task RegisterEvent(CreateEventRegistrationDto eventRegistrationDto)
        {
            await registrationsRepository.RegisterEvent(
                    new UserDto()
                    {
                        FirstName = eventRegistrationDto.FirstName,
                        LastName = eventRegistrationDto.LastName,
                        DateOfBirth = eventRegistrationDto.DateOfBirth,
                    },
                    new CompanyDto()
                    {
                        CompanyName = eventRegistrationDto.CompanyName,
                        Domain = eventRegistrationDto.CompanyDomain
                    });

            // Make the first asynchronous call, wait random time with small chance to fail
            var task1 = httpClient.GetAsync($"https://httpstat.us/{(new Random().Next(8) == 0 ? "200" : "500")}?sleep={rnd.Next(2000) + 50}");

            // Make the second asynchronous call, wait random time
            var task2 = httpClient.GetAsync($"https://httpstat.us/{(new Random().Next(5) == 0 ? "201" : "500")}? sleep={rnd.Next(2000) + 50}");

            // Wait for both calls to complete
            var responses = await Task.WhenAll(task1, task2);

            var errors = new List<Exception>();
            for(int i = 0; i < responses.Length; i++)
            {
                if (!responses[i].IsSuccessStatusCode)
                {
                    errors.Add(new Exception($"Simulated failure occured on response {i + 1}/{responses.Length}"));
                }
            }

            if(errors.Count == 1)
                throw errors[0];
            else if(errors.Count > 1)
            {
                throw new AggregateException("Multiple api cals failed", errors.ToArray());
            }
        }

        public async Task ClearEventRegistrations()
        {
            await registrationsRepository.ClearRegistrations();
        }

        public async Task<EventRegistrationsResult> GetEventRegistrationSummary()
        {
            return await registrationsRepository.GetEventRegistrationsSummaryAsync();
        }

        public bool IsRegistrationAvailable()
        {
            var totalRegistrations = registrationsRepository.GetTotalRegistrations();

            return totalRegistrations < applicationConfiguration.MaxRegistrations;
        }
    }
}
