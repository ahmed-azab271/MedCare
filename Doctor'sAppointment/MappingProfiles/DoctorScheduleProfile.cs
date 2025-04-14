using AutoMapper;
using DAL.Models;
using Doctor_sAppointment.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace Doctor_sAppointment.MappingProfiles
{
    public class DoctorScheduleProfile : Profile
    {
        public DoctorScheduleProfile()
        {
            CreateMap<DoctorScheduleVM , DoctorSchedule>().ReverseMap();
        }
    }
}
