using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LiveTest.Models
{
    public class Courses
    {
        [Key]
       public int CourseId { get; set; }
        [DisplayName("Course Name")]
        [Required]
        public string CourseName { get; set; }
        [Required]
        [DisplayName("Course Duration")]
        public int CourseDuration { get; set; }
    }
}
