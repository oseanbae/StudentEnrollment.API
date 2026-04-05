using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.DTOs
{
    public class CourseCreateDTO
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [Required, Range(1, 6)]
        public int Credits { get; set; }
    }
}