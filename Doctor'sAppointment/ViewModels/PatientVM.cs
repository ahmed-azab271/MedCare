using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class PatientVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Appointments { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        public string? Duration { get; set; }

        public string? Severity { get; set; }

        public string? AdditionalNotes { get; set; }

        [StringLength(200, ErrorMessage = "Diagnosis cannot exceed 200 characters")]
        public string? Diagnosis { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Treatment { get; set; }

        [StringLength(100, ErrorMessage = "Doctor's name cannot exceed 100 characters")]
        [Display(Name = "Treating Doctor")]
        public string? Doctor { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }



        public string AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? DoctorModel { get; set; }

        public ICollection<DoctorSchedule>? DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
    }

}
