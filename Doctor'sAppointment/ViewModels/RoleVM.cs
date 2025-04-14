using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class RoleVM
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public RoleVM()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
