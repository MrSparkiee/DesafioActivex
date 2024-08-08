using DesafioActivex.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DesafioActivex.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }

    }
}
