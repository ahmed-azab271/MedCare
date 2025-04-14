using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class DoctorVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [RegularExpression ("Cairo|Giza|Alex|Fayoum") ]
        public string City { get; set; }
        [RegularExpression ("General Medicine|Internal Medicine|Family Medicine|Radiology|Anesthesiology|Pathology|Forensic Medicine|Geriatrics|Sports Medicine")]
        public string Specialty { get; set; }
        public int Fees { get; set; }
        public int ExperinceYears { get; set; }
        public string? Resume { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Patient>? Patients { get; set; } = new HashSet<Patient>();
    }
}
