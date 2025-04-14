using Microsoft.AspNetCore.Identity;

namespace Doctor_sAppointment.ViewModels
{
    public class UsersVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Specialty { get; set; }
        public bool Agree { get; set; }
        public string OldRole { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Roles { get; set; }
       
    }
}
