using System;
using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class UserForEditDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string IDNumber { get; set; }
    }
}