using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class AccountUserVM
    {
        [Required]
        [MinLength(7)]
        public string FullName { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordCheck { get; set; }
        public string? oldRole { get; set; }
        public bool Agree { get; set; }
    }
}
