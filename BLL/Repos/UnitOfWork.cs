using BLL.Interfaces;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repos
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly DoctorDbContext dbContext;
        public IDoctorRepo DoctorRepo { get; set; }
        public IDoctorScheduleRepo DoctorScheduleRepo { get; set; }
        public IPatientRepo PatientRepo { get; set; }
        public UnitOfWork(DoctorDbContext _dbContext)
        {
            DoctorRepo = new DoctorRepo(_dbContext);
            DoctorScheduleRepo = new DoctorScheduleRepo(_dbContext);
            PatientRepo = new PatientRepo(_dbContext);
            dbContext = _dbContext;
        }

        public async Task<int> Compelete() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose();   
    }
}
