using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.BLL.Dto;
using Clinic.BLL.Interfaces;
using Clinic.DAL.Domain;
using Clinic.DAL.Interfaces;

namespace Clinic.BLL.Services
{
    internal class TimeSlotService : IService<TimeSlotDto>
    {
        private readonly IRepository<TimeSlot> _timeSlotRepository;
        private readonly IMapper _mapper;

        public TimeSlotService(IRepository<TimeSlot> timeSlotRepository, IMapper mapper)
        {
            _timeSlotRepository = timeSlotRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(TimeSlotDto item)
        {
            var addedTimeSlot = await _timeSlotRepository.AddAsync(_mapper.Map<TimeSlot>(item));

            return addedTimeSlot.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _timeSlotRepository.DeleteAsync(id);
        }

        public List<TimeSlotDto> GetAll()
        {
            var timeSlots = _timeSlotRepository.GetAll();

            return _mapper.Map<List<TimeSlotDto>>(timeSlots.ToList());
        }

        public async Task<TimeSlotDto> GetAsync(int id)
        {
            var timeSlot = await _timeSlotRepository.GetAsync(id);

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }

        public async Task UpdateAsync(TimeSlotDto item)
        {
            await _timeSlotRepository.UpdateAsync(_mapper.Map<TimeSlot>(item));
        }
    }
}
