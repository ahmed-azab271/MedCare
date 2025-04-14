using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AccountUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordCheck { get; set; }
        public string? oldRole { get; set; }
        
        public bool Agree { get; set; }
    }
}
