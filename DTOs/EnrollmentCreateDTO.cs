using System.ComponentModel.DataAnnotations;
namespace StudentEnrollment.API.DTOs
{
    public class EnrollmentCreateDTO
    {
        [Required]
        public long StudentId { get; set; }

        [Required]
        public long CourseId { get; set; }
    }
}
