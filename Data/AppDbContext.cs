using Microsoft.EntityFrameworkCore;
using StudentEnrollment.API.Models;

namespace StudentEnrollment.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
    }
}
