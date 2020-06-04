using AutoMapper;
using Clinic.BLL.Dto;
using Clinic.WebUI.Models;
using Microsoft.AspNetCore.Identity;

namespace Clinic.WebUI.Infrastructure
{
    public class UIMapperProfile : Profile
    {
        public UIMapperProfile()
        {
            CreateMap<Patient, PatientViewModel>().ReverseMap();
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();
            CreateMap<Doctor, DoctorViewModel>().ReverseMap();
            CreateMap<Reception, ReceptionViewModel>().ReverseMap();
        }
    }
}
