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
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Date Of Birth")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(40,MinimumLength =8,ErrorMessage ="length must be atleast {2} characters")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Password must be at least 8 characters long, include uppercase and lowercase letters, a digit, and a special character.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password",ErrorMessage ="password doesnot match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } = "user";

    }
}
