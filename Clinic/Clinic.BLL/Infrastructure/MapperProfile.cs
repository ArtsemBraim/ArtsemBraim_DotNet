using AutoMapper;
using Clinic.DAL.Domain;
using DTO = Clinic.BLL.Dto;

namespace Clinic.BLL.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Doctor, DTO.Doctor>().ReverseMap();
            CreateMap<Patient, DTO.Patient>().ReverseMap();
            CreateMap<Reception, DTO.Reception>().ReverseMap();
        }
    }
}
