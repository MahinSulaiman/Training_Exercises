using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRegistrationForm.Models
{
	public class Users
	{
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("DOB")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public long? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30)]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }
    }
}