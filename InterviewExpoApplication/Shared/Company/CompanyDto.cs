using System.ComponentModel.DataAnnotations;
using InterviewExpoApplication.Shared.User;

namespace InterviewExpoApplication.Shared.Company
{
    public class CompanyDto : IEquatable<CompanyDto>
    {
        public int Id { get; set; }


        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 4)]
        public string Domain { get; set; }

        public List<UserDto> Users { get; set; }

        public bool Equals(CompanyDto other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(CompanyName, other.CompanyName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Domain, other.Domain, StringComparison.OrdinalIgnoreCase);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(CompanyName, Domain);
        }
    }
}
