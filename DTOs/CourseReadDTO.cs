namespace StudentEnrollment.API.DTOs
{
    public class CourseReadDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public int Credits { get; set; }
    }
}
