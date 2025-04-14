using AutoMapper;
using DAL.Models;
using Doctor_sAppointment.ViewModels;

namespace Doctor_sAppointment.MappingProfiles
{
    public class DoctorPtofile : Profile
    {
        public DoctorPtofile()
        {
            CreateMap<DoctorVM , Doctor>().ReverseMap();
        }
    }
}
