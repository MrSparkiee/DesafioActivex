using DesafioActivex.Models;
using Microsoft.AspNetCore.Mvc;

namespace DesafioActivex.Interfaces
{
    public interface ICoursesController
    {
        Task<ActionResult<IEnumerable<Course>>> GetCourses();
        Task<ActionResult<Course>> GetCourse(string id);
        Task<IActionResult> PutCourse(string id, Course course);
        Task<ActionResult<Course>> PostCourse(Course course);
        Task<IActionResult> DeleteCourse(string id);
    }
}
