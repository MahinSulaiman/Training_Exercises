using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaysysLearningPortal.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DisplayName("DOB")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(40,MinimumLength =8,ErrorMessage ="length must be atleast {2} characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password",ErrorMessage ="password doesnot match")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "user";

    }
}
