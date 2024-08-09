using Azure.Core;
using DesafioActivex.Controllers;
using DesafioActivex.Data;
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
    public class CoursesControllerTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Course.CountAsync() <= 0)
            {

                databaseContext.Course.Add(new Course()
                {
                    CourseID = "C001",
                    Name = "Curso A"
                });

                databaseContext.Course.Add(new Course()
                {
                    CourseID = "C002",
                    Name = "Curso B"
                });

                databaseContext.Course.Add(new Course()
                {
                    CourseID = "C003",
                    Name = "Curso C"
                });

                await databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public async void CoursesController_GetCourses_ReturnAllCourses()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);

            var result = courseController.GetCourses();

            result.Result.Value.Should().NotBeNull();
            result.Result.Value.Should().BeOfType<List<Course>>().Which.Count.Should().Be(3);
        }

        [Fact]
        public async void CoursesController_GetCourse_ReturnCourse()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);

            var result = courseController.GetCourse("C001");

            result.Result.Value.Should().NotBeNull();
            result.Result.Value.Should().BeOfType<Course>();
        }

        [Fact]
        public async void CoursesController_GetCourse_ReturnNull()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);

            var result = courseController.GetCourse("C999");

            result.Result.Value.Should().BeNull();
        }

        [Fact]
        public async void CoursesController_PutCourse_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var existingCourse = await dbContext.Course.FirstOrDefaultAsync(c => c.CourseID == "C001");
            dbContext.Entry(existingCourse).State = EntityState.Detached;

            var courseToUpdate = new Course { CourseID = "C001", Name = "Curso Z" };

            var result = await courseController.PutCourse("C001", courseToUpdate);

            result.Should().BeOfType<NoContentResult>();

            var updatedCourse = await dbContext.Course.FindAsync("C001");
            updatedCourse.Name.Should().Be("Curso Z");
        }

        [Fact]
        public async void CoursesController_PutCourse_IdDoesNotMatch()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var course = new Course { CourseID = "C002", Name = "Curso B" };

            var result = await courseController.PutCourse("C001", course);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async void CoursesController_PutCourse_DoesNotExist()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var course = new Course { CourseID = "C999", Name = "Curso Z" };

            var result = await courseController.PutCourse("C999", course);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void CoursesController_PostCourse_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var course = new Course { CourseID = "C004", Name = "Curso D" };

            var result = await courseController.PostCourse(course);

            result.Result.Should().BeOfType<CreatedAtActionResult>();

            var createdCourse = await dbContext.Course.FindAsync("C004");
            createdCourse.Should().NotBeNull();
            createdCourse.Name.Should().Be("Curso D");
        }

        [Fact]
        public async void CoursesController_PostCourse_ReturnsConflict()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var existingCourse = await dbContext.Course.FirstOrDefaultAsync(c => c.CourseID == "C001");
            dbContext.Entry(existingCourse).State = EntityState.Detached;

            var course = new Course { CourseID = "C001", Name = "Curso A" };


            Func<Task> act = async () => await courseController.PostCourse(course);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async void CoursesController_DeleteCourse_ReturnsSuccess()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var courseID = "C001";

            var result = await courseController.DeleteCourse(courseID);

            result.Should().BeOfType<NoContentResult>();

            var courseExists = await dbContext.Course.AnyAsync(c => c.CourseID == courseID);
            courseExists.Should().BeFalse();
        }

        [Fact]
        public async void CoursesController_DeleteCourse_ReturnsNotFound()
        {
            var dbContext = await GetDatabaseContext();
            var courseController = new CoursesController(dbContext);
            var courseID = "C999";

            var result = await courseController.DeleteCourse(courseID);

            result.Should().BeOfType<NotFoundResult>();
        }

    }
}
