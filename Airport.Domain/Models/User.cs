﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [Encrypted] public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Privileges { get; set; }
        [Encrypted] public string FirstName { get; set; }
        [Encrypted] public string LastName { get; set; }
        [Encrypted] public string Country { get; set; }
        [Encrypted] public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        [Encrypted] public string IDNumber { get; set; }
        public bool IsConfirmed { get; set; }
        [Encrypted] public string RegistrationToken { get; set; }
        public DateTime RegistrationTokenGeneratedTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual IEnumerable<Booking> Bookings { get; set; }        
    }
}
