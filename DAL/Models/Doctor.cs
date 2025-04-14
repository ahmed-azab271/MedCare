using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public string Specialty { get; set; }
        public string Fees { get; set; }
        public int ExperinceYears { get; set; }
        public string? Resume { get; set; }
        public string? ImageName { get; set; }


        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Patient>? Patients { get; set; } = new HashSet<Patient>();
    }
}
