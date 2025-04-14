using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class ResetPassVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
