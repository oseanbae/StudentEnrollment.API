namespace StudentEnrollment.API.DTOs
{
    public class EnrollmentReadDTO
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public required string StudentName { get; set; }
        public required string CourseTitle { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
