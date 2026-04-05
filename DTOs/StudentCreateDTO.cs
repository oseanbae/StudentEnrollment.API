using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.DTOs
{
    public class StudentCreateDTO
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, StringLength(254, MinimumLength = 5)]
        public string Email { get; set; } = null!;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }
    }
}
