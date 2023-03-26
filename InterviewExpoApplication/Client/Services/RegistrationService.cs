using InterviewExpoApplication.Shared.Company;
using InterviewExpoApplication.Shared.Configuration;
using InterviewExpoApplication.Shared.EventRegistration;
using System.Net.Http.Json;
using System.Text.Json;

namespace InterviewExpoApplication.Client.Services
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public const string API_KEY = "sk_5b0c60f2800d28fb7ca9c49a86c792ca";

        public RegistrationService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterAttendance(CreateEventRegistrationDto registrationDto)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + API_KEY);
            var companyLookup = await _httpClient.GetAsync($"https://autocomplete.clearbit.com/v1/domains/find?name={registrationDto.CompanyName}");

            if (companyLookup.IsSuccessStatusCode)
            {
                var contentJson = await companyLookup.Content.ReadAsStringAsync(); 

                var companyLookupResult = JsonSerializer.Deserialize<CompanyLookupResult>(contentJson, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                registrationDto.CompanyDomain = companyLookupResult?.Domain;
            }

            var createResponse = await _httpClient.PostAsJsonAsync("api/registration", registrationDto);

            return createResponse;
        }

        public async Task<EventRegistrationsResult> GetSummary()
        {
            return await _httpClient.GetFromJsonAsync<EventRegistrationsResult>("api/registration");
        }

        public async Task ClearRegistrations()
        {
            await _httpClient.DeleteAsync("api/registration");
        }

        public async Task<bool> RegistrationsAvailable()
        {
            var response = await _httpClient.GetAsync("api/registration/available");

            if(response.IsSuccessStatusCode)
            {
                var availableString = await response.Content.ReadAsStringAsync();

                if (bool.TryParse(availableString, out bool available))
                {
                    return available;
                }
            }
            return false;
        }
    }
}
