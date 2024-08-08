using DesafioActivex.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioActivex.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Student { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course() { CourseID = "C001", Name = "Curso A"},
                new Course() { CourseID = "C002", Name = "Curso B"},
                new Course() { CourseID = "C003", Name = "Curso C"}
            );

            modelBuilder.Entity<Student>().HasData(
                new Student() { StudentID = "S001", Name = "Aluno A", Email = "alunoA@mail.com", CourseID = "C001" },
                new Student() { StudentID = "S002", Name = "Aluno B", Email = "alunoB@mail.com", CourseID = "C001" },
                new Student() { StudentID = "S003", Name = "Aluno C", Email = "alunoC@mail.com", CourseID = "C002" }
            );
        }
    }
}
