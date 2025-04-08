using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaysysLearningPortal.Models
{
    public class Courses
    {
        [Key]
        public Guid CourseId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Title { get; set; }
        [Required]
        [DisplayName("Upload image")]
        //public IFormFile CourseImage { get; set; }
        public byte[] CourseImage { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayName("Course URL")]
        public string CourseFile { get; set; }
        [Required]
        public Guid CategoryId { get; set; }

        public string? EnrollStatus { get; set; } 

        public string? Category { get; set; }

        
        public string ExistingImage { get; set; }

    }
}
