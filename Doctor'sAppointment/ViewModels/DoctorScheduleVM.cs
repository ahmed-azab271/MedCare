using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Doctor_sAppointment.ViewModels
{
    public class DoctorScheduleVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public string Day { get; set; }
        [DataType(DataType.Time)]
        public TimeOnly StartAt { get; set; }
        public TimeOnly EndAt => StartAt.AddMinutes(30);


        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
  
}
