using System.ComponentModel.DataAnnotations;

namespace InterviewExpoApplication.Shared.EventRegistration
{
    public class EventRegistration : BaseEventRegistration
    {

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string CompanyDomain { get; set; }
    }
}
