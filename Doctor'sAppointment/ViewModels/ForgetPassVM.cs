using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class ForgetPassVM
    {
        [Required]
        public string UserName { get; set; }
    }
}
