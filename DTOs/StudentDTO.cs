using System.ComponentModel.DataAnnotations;

namespace StudentEnrollment.API.DTOs
{
    public class StudentDTO
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

        public class StudentReadDTO
        {
            public long Id { get; set; }
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? PhoneNumber { get; set; }
        }
        public class StudentUpdateDTO : StudentCreateDTO
        {
            
        }
    }
}
