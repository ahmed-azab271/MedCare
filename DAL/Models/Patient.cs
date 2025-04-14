using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Appointments { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public string? Severity { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Doctor { get; set; }
        public string? Notes { get; set; }

        public string? AdditionalNotes { get; set; }



        public string AccountUserId { get; set; }
        public AccountUser? AccountUser { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? DoctorModel { get; set; }

        public ICollection<DoctorSchedule>? DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
    }

        
    
}
