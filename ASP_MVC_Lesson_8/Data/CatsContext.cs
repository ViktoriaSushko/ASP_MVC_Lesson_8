using ASP_MVC_Lesson_8.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC_Lesson_8.Data
{
    public class CatsContext:DbContext

    {
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public CatsContext(DbContextOptions<CatsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
