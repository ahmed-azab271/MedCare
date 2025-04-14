using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL.Contexts
{
    public class DoctorDbContext : IdentityDbContext<AccountUser>
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedule { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public DoctorDbContext(DbContextOptions<DoctorDbContext> options): base(options)
        {
        }
    }
}
