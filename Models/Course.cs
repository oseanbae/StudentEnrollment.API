using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace StudentEnrollment.API.Models
{
    public class Course
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public required string Title { get; set; }
        [Range(1, 6)]
        public required int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = null!;
    }
}
