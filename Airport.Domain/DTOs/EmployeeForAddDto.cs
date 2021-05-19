using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class EmployeeForAddDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 40 characters")]
        public string Password { get; set; }
        [Required]
        public int Privileges { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}