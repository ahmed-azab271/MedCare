using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPatientRepo : IGenericRepository<Patient>
    {
        Patient GetPatientById(string id);
        Patient GetPatientWithSchedualeById(string id);
        Patient GetPatientAllIncludedById(string id);
    }
}
