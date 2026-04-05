namespace StudentEnrollment.API.DTOs
{
    public class StudentReadDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
