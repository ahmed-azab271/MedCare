using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool RememberMe{ get; set; }
    }
}
