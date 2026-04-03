using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.Models
{
    public class Student
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? throw new ArgumentNullException(nameof(Name));
        }
        private string _name = null!;

        [Required, EmailAddress, StringLength(254, MinimumLength = 5)]
        public string Email
        {
            get => _email;
            set => _email = value?.Trim().ToLower() ?? throw new ArgumentNullException(nameof(Email));
        }
        private string _email = null!;

        [StringLength(15)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber
        {
            get => _phoneNumber;
            set => _phoneNumber = value?.Trim();
        }
        private string? _phoneNumber;

        public ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }
}