using ExamAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ExamAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Marks> Marks { get; set; }

    }
}
