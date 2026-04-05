using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.API.Data;
using StudentEnrollment.API.Models;
using StudentEnrollment.API.DTOs;
using StudentEnrollment.API.Helpers;

namespace StudentEnrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDTO>>> GetAllCourse()
        {
            return await _context.Courses
                .Select(c => MappingHelper.CourseToDTO(c))
                .ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDTO>> GetCourseById(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound($"Course with ID: {id} doesnt exist");
            return MappingHelper.CourseToDTO(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(long id, CourseUpdateDTO courseDTO)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound($"Course with ID: {id} doesnt exist");

            course.Title = courseDTO.Title;
            course.Credits = courseDTO.Credits;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                    return NotFound($"Course with ID: {id} doesnt exist");
                return Conflict("Concurrency conflict occurred while updating the course.");

            }

            return NoContent();
        }

        // GET /api/courses/{id}/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentReadDTO>>> GetCourseStudents(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound($"Course with ID: {id} doesnt exist");

            return await _context.Enrollments
                .Include(e => e.Student)
                .Where(e => e.CourseId == id)
                .Select(s => MappingHelper.StudentToDTO(s.Student))
                .ToListAsync();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseReadDTO>> CreateCourse(CourseCreateDTO courseDTO)
        {
            if (await _context.Courses.AnyAsync(c => c.Title == courseDTO.Title))
                return BadRequest("A course with this title already exists.");

            var newCourse = new Course()
            {
                Title = courseDTO.Title,
                Credits = courseDTO.Credits,
            };
            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseById), new { id = newCourse.Id }, MappingHelper.CourseToDTO(newCourse));
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(long id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID: {id} doesnt exist");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(long id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
