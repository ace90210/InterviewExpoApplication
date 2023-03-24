using InterviewExpoApplication.Shared.EventRegistration;
using System.Net.Http.Json;

namespace InterviewExpoApplication.Client.Services
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public RegistrationService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> RegisterAttendance(CreateEventRegistrationDto registrationDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/registration", registrationDto);

            return response;
        }
    }
}
