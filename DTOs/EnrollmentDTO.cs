using System.ComponentModel.DataAnnotations;
namespace StudentEnrollment.API.DTOs
{
    public class EnrollmentDTO
    {
        public class EnrollmentCreateDTO
        {
            [Required]
            public long StudentId { get; set; }

            [Required]
            public long CourseId { get; set; }
        }

        public class EnrollmentReadDTO
        {
            public long Id { get; set; }
            public long StudentId { get; set; }
            public long CourseId { get; set; }
            public DateTime EnrollmentDate { get; set; }
        }
    }
}
