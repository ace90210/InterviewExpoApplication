using InterviewExpoApplication.Shared.EventRegistration;

namespace InterviewExpoApplication.Server.Services
{
    public interface IRegistrationService
    {
        Task RegisterEvent(CreateEventRegistrationDto eventRegistrationDto);

        Task<EventRegistrationsResult> GetEventRegistrationSummary();

        Task ClearEventRegistrations();

        bool IsRegistrationAvailable();

    }
}
