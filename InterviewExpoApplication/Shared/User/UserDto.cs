using System.ComponentModel.DataAnnotations;

namespace InterviewExpoApplication.Shared.User
{
    public class UserDto : IEquatable<UserDto>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        public bool Equals(UserDto other)
        {
            if (other == null)
            {
                return false;
            }

            return string.Equals(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(LastName, other.LastName, StringComparison.OrdinalIgnoreCase) &&
                   DateOfBirth.Date == other.DateOfBirth.Date;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, DateOfBirth.Date);
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, DoB: {DateOfBirth.ToShortDateString()}";
        }

    }
}
