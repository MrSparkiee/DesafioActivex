using DesafioActivex.Controllers;
using DesafioActivex.Data;
using DesafioActivex.Interfaces;
using DesafioActivex.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioActivex.Tests.Controllers
{
    public class StudentsControllerTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Student.CountAsync() <= 0)
            {
                databaseContext.Course.Add(new Course()
                {
                    CourseID = "C001",
                    Name = "Curso A"
                });

                databaseContext.Student.Add(new Student()
                {
                    StudentID = "S001",
                    Name = "Aluno A",
                    Email = "mail@mail.com",
                    CourseID = "C001",
                });

                databaseContext.Student.Add(new Student()
                {
                    StudentID = "S002",
                    Name = "Aluno B",
                    Email = "mail@mail.com",
                    CourseID = "C001",
                });

                databaseContext.Student.Add(new Student()
                {
                    StudentID = "S003",
                    Name = "Aluno C",
                    Email = "mail@mail.com",
                    CourseID = "C001",
                });



                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async void StudentsController_GetStudents_ReturnAllStudents()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);

            var result = studentsController.GetStudents();

            result.Result.Value.Should().NotBeNull();
            result.Result.Value.Should().BeOfType<List<Student>>().Which.Count.Should().Be(3);
        }

        [Fact]
        public async void StudentsController_GetStudent_ReturnStudent()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);

            var result = studentsController.GetStudent("S001");

            result.Result.Value.Should().NotBeNull();
            result.Result.Value.Should().BeOfType<Student>();
        }

        [Fact]
        public async void StudentsController_GetStudent_ReturnNull()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);

            var result = studentsController.GetStudent("C999");

            result.Result.Value.Should().BeNull();
        }

        [Fact]
        public async void StudentsController_PutStudent_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var existingStudent = await dbContext.Student.FirstOrDefaultAsync(s => s.StudentID == "S001");
            dbContext.Entry(existingStudent).State = EntityState.Detached;

            var studentToUpdate = new Student
            {
                StudentID = "S001",
                Name = "Aluno Z",
                Email = "mail@mail.com",
                CourseID = "C001",
            };

            var result = await studentsController.PutStudent("S001", studentToUpdate);

            result.Should().BeOfType<NoContentResult>();

            var updatedStudent = await dbContext.Student.FindAsync("S001");
            updatedStudent.Name.Should().Be("Aluno Z");
        }

        [Fact]
        public async void StudentsController_PutStudent_IdDoesNotMatch()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var student = new Student
            {
                StudentID = "S002",
                Name = "Aluno Z",
                Email = "mail@mail.com",
                CourseID = "C001",
            };

            var result = await studentsController.PutStudent("S001", student);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void StudentsController_PutStudent_DoesNotExist()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var student = new Student
            {
                StudentID = "S999",
                Name = "Aluno Z",
                Email = "mail@mail.com",
                CourseID = "C001",
            };

            var result = await studentsController.PutStudent("S999", student);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void StudentsController_PostStudent_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var student = new Student
            {
                StudentID = "S004",
                Name = "Aluno D",
                Email = "mail@mail.com",
                CourseID = "C001",
            };

            var result = await studentsController.PostStudent(student);

            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var createdStudent = await dbContext.Student.FindAsync("S004");
            createdStudent.Should().NotBeNull();
            createdStudent.Name.Should().Be("Aluno D");
        }

        [Fact]
        public async void StudentsController_PostStudent_ReturnsConflict()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var existingStudent = await dbContext.Student.FirstOrDefaultAsync(s => s.StudentID== "S001");
            dbContext.Entry(existingStudent).State = EntityState.Detached;

            var student = new Student
            {
                StudentID = "S001",
                Name = "Aluno A",
                Email = "mail@mail.com",
                CourseID = "C001",
            };


            Func<Task> act = async () => await studentsController.PostStudent(student);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void StudentsController_DeleteStudent_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var studentID = "S001";

            var result = await studentsController.DeleteStudent(studentID);

            result.Should().BeOfType<NoContentResult>();

            var studentExists = await dbContext.Student.AnyAsync(s => s.StudentID == studentID);
            studentExists.Should().BeFalse();
        }

        [Fact]
        public async void StudentsController_DeleteStudent_ReturnsNotFound()
        {
            var dbContext = await GetDatabaseContext();
            var studentsController = new StudentsController(dbContext);
            var studentID = "S999";

            var result = await studentsController.DeleteStudent(studentID);

            result.Should().BeOfType<NotFoundResult>();
        }


    }
}
