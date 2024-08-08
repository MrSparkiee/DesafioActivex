using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioActivex.Models
{
    public class Course
    {
        [Key]
        public required string CourseID { get; set; }

        [Required(ErrorMessage = "The field name is required!")]
        [StringLength(50)]
        public required string Name { get; set; }

       
        public ICollection<Student>? Students { get; set; }
    }
}
