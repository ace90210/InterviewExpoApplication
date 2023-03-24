using System.ComponentModel.DataAnnotations;

namespace InterviewExpoApplication.Shared.EventRegistration
{
    public class BaseEventRegistration
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be at least 2 characters and no more than 50 in length")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be at least 2 characters and no more than 50 in length")]
        public string LastName { get; set; }


        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Company name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Company name must be at least 2 characters and no more than 200 in length")]
        public string CompanyName { get; set; }
    }
}
