using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CollegeApp.Validators;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [Remote(action: "VerifyEmail", controller: "Student", AdditionalFields = nameof(Id))]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }

        public DateTime DOB { get; set; }
    }
}
