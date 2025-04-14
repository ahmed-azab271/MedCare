using AutoMapper;
using DAL.Models;
using Doctor_sAppointment.ViewModels;

namespace Doctor_sAppointment.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AccountUser , UsersVM>().ReverseMap();
        }
    }
}
