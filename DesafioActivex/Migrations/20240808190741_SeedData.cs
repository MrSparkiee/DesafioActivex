using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DesafioActivex.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Name" },
                values: new object[,]
                {
                    { "C001", "Curso A" },
                    { "C002", "Curso B" },
                    { "C003", "Curso C" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "CourseID", "Email", "Name" },
                values: new object[,]
                {
                    { "S001", "C001", "alunoA@mail.com", "Aluno A" },
                    { "S002", "C001", "alunoB@mail.com", "Aluno B" },
                    { "S003", "C002", "alunoC@mail.com", "Aluno C" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: "C003");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: "S001");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: "S002");

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: "S003");

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: "C001");

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: "C002");
        }
    }
}
