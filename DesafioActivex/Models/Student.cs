using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioActivex.Models
{
    public class Student
    {
        [Key]
        public required string StudentID { get; set; }

        [Required(ErrorMessage = "The field name is required!")]
        [StringLength(50)]
        public required string Name { get; set;}

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Please insert a valid email!")]
        public string? Email { get; set; }

        public required string CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course? Course { get; set; }

    }
}
