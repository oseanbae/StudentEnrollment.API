using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.API.Data;
using StudentEnrollment.API.Models;
using StudentEnrollment.API.DTOs;
using StudentEnrollment.API.Helpers;

namespace StudentEnrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDTO>>> GetAllStudent()
        {
            var students = await _context.Students
                .Select(s => MappingHelper.StudentToDTO(s))
                .ToListAsync();
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDTO>> GetStudentById(long id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound($"Student with ID: {id} doesnt exist");
            }

            return MappingHelper.StudentToDTO(student);
        }

        // GET: api/students/{id}/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseReadDTO>>> GetStudentCourses(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound($"Student with ID: {id} doesnt exist");

            return await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == id)
                .Select(c => MappingHelper.CourseToDTO(c.Course))
                .ToListAsync();
        }
         
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(long id, StudentUpdateDTO studentDTO)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            student.Name = studentDTO.Name;
            student.Email = studentDTO.Email;
            student.PhoneNumber = studentDTO.PhoneNumber; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                    return NotFound();
                return Conflict("Concurrency conflict occurred while updating the student.");
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentReadDTO>> CreateStudent(StudentCreateDTO studentDTO)
        {
            if(await _context.Students.AnyAsync(s => s.Email == studentDTO.Email))
                return BadRequest("A student with this email already exists.");

            var newStudent = new Student
            {
                Name = studentDTO.Name,
                Email = studentDTO.Email,
                PhoneNumber = studentDTO.PhoneNumber,

            };
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, MappingHelper.StudentToDTO(newStudent));
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
