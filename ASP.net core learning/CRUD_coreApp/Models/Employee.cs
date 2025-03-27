using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CRUD_coreApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        [Required]
        public  string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public  string LastName { get; set; }

        [Required]
        [DisplayName("DOB")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public double Salary { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
