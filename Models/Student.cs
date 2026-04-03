using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.Models
{
    public class Student
    {
        public long Id { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        [EmailAddress]
        [MaxLength(254)]
        public required string Email { get; set; }
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = null!;

    }
}
