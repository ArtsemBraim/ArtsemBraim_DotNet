using AutoMapper;
using Clinic.BLL.Dto;
using Clinic.DAL.Domain;

namespace Clinic.BLL.Infrastructure
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<Specialization, SpecializationDto>().ReverseMap();
            CreateMap<TimeSlot, TimeSlotDto>().ReverseMap();

        }
    }
}
