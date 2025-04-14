using BLL.Interfaces;
using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class PatientRepo : GenericRepository<Patient>, IPatientRepo
    {
        private readonly DoctorDbContext dbContext;

        public PatientRepo(DoctorDbContext _dbContext):base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public Patient GetPatientAllIncludedById(string id)
            => dbContext.Patients.Include(S => S.DoctorSchedules).Include(D=>D.DoctorModel).FirstOrDefault(I => I.Id == id);

        public Patient GetPatientById(string id)
        {
            var patient = dbContext.Patients.FirstOrDefault(I => I.Id == id);
            return patient;
        }

        public Patient GetPatientWithSchedualeById(string id)
            => dbContext.Patients.Include(S => S.DoctorSchedules).FirstOrDefault(I => I.Id == id);
    }
}
