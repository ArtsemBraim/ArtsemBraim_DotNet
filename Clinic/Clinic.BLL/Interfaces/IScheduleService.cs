using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinic.BLL.Dto;

namespace Clinic.BLL.Interfaces
{
    public interface IScheduleService
    {
        List<TimeSlotDto> GetAvailableTimeSlots(DoctorDto doctor);

        Task<ScheduleDto> BuildScheduleAsync(DoctorDto doctor, DateTime beginTime, DateTime endTime, int interval);

        Task<ScheduleDto> GetByIdAsync(int id);

        Task UpdateScheduleAsync(ScheduleDto schedule);

        Task DeleteScheduleAsync(int id);
    }
}