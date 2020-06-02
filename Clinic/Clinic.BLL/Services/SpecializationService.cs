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
    internal class SpecializationService : IService<SpecializationDto>
    {
        private readonly IRepository<Specialization> _specializationRepository;
        private readonly IMapper _mapper;

        public SpecializationService(IRepository<Specialization> specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(SpecializationDto item)
        {
            var addedSpecialization = await _specializationRepository.AddAsync(_mapper.Map<Specialization>(item));

            return addedSpecialization.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _specializationRepository.DeleteAsync(id);
        }

        public List<SpecializationDto> GetAll()
        {
            var specializations = _specializationRepository.GetAll();

            return _mapper.Map<List<SpecializationDto>>(specializations.ToList());
        }

        public async Task<SpecializationDto> GetAsync(int id)
        {
            var specialization = await _specializationRepository.GetAsync(id);

            return _mapper.Map<SpecializationDto>(specialization);
        }

        public async Task UpdateAsync(SpecializationDto item)
        {
            await _specializationRepository.UpdateAsync(_mapper.Map<Specialization>(item));
        }
    }
}
