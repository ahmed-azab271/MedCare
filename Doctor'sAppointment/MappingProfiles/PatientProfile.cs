using AutoMapper;
using DAL.Models;
using Doctor_sAppointment.ViewModels;

namespace Doctor_sAppointment.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient , PatientVM>().ReverseMap();
        }
    }
}
