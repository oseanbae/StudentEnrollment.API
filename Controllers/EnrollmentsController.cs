using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StudentEnrollment.API.Data;
using StudentEnrollment.API.Models;
using static StudentEnrollment.API.DTOs.EnrollmentDTO;

namespace StudentEnrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDTO>>> GetAllEnrollment()
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Select(e =>EnrollmentToDTO(e))
                .ToListAsync();
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentReadDTO>> GetEnrollmentById(long id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);
                
            if (enrollment == null) return NotFound("Enrollment doesnt exist");
            return EnrollmentToDTO(enrollment);
        }
        
        //
        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EnrollmentReadDTO>> CreateEnrollment(EnrollmentCreateDTO enrollmentDTO)
        {
            var student = await _context.Students.FindAsync(enrollmentDTO.StudentId);
            var course = await _context.Courses.FindAsync(enrollmentDTO.CourseId);

            if (student == null) return NotFound($"Student with ID {enrollmentDTO.StudentId} not found.");
            if (course == null) return NotFound($"Course with ID {enrollmentDTO.CourseId} not found.");
            if (await _context.Enrollments.AnyAsync(e => e.StudentId == enrollmentDTO.StudentId &&
                        e.CourseId == enrollmentDTO.CourseId))
                return Conflict("Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDTO.StudentId,
                CourseId = enrollmentDTO.CourseId,
                EnrollmentDate = DateTime.UtcNow,
                Student = student,
                Course = course
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, EnrollmentToDTO(enrollment));
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(long id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static EnrollmentReadDTO EnrollmentToDTO(Enrollment enrollment)
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
