using StudentEnrollment.API.DTOs;
using StudentEnrollment.API.Models;

namespace StudentEnrollment.API.Helpers
{
    public class MappingHelper
    {
        public static StudentReadDTO StudentToDTO(Student student)
        {
            return new StudentReadDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
            };
        }

        public static CourseReadDTO CourseToDTO(Course course)
        {
            return new CourseReadDTO
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits
            };
        }
        public static EnrollmentReadDTO EnrollmentToDTO(Enrollment enrollment)
        {
            return new EnrollmentReadDTO
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                StudentName = enrollment.Student.Name,
                CourseTitle = enrollment.Course.Title,
                EnrollmentDate = enrollment.EnrollmentDate,
            };
        }
    }
}
