using InterviewExpoApplication.Shared.Company;

namespace InterviewExpoApplication.Shared.EventRegistration
{
    public class EventRegistrationsResult
    {
        public List<CompanyDto> Companies { get; set; }

        public int TotalRegistrants { get; set; }
    }
}
