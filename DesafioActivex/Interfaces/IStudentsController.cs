using DesafioActivex.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioActivex.Interfaces
{
    public interface IStudentsController
    {
        Task<ActionResult<IEnumerable<Student>>> GetStudents();
        Task<ActionResult<Student>> GetStudent(string id);
        Task<IActionResult> PutStudent(string id, Student student);
        Task<ActionResult<Student>> PostStudent(Student student);
        Task<IActionResult> DeleteStudent(string id);
    }
}
