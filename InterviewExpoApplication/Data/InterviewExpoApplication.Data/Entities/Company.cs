using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterviewExpoApplication.Data.Entities
{
    public class Company
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string CompanyName { get; set; }

        public string? CompanyDomain { get; set; }

        public List<UserCompany> UserCompanies { get; set; }
    }
}
