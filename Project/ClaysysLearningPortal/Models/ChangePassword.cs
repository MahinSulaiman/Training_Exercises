using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ClaysysLearningPortal.Models
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "length must be atleast {2} characters")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be at least 8 characters long, include uppercase and lowercase letters, a digit, and a special character.")]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("NewPassword", ErrorMessage = "password doesnot match")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
