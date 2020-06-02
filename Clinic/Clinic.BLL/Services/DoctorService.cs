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
    internal class DoctorService : IService<DoctorDto>
    {
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly IMapper _mapper;

        public DoctorService(IRepository<Doctor> doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(DoctorDto item)
        {
            var addedDoctor = await _doctorRepository.AddAsync(_mapper.Map<Doctor>(item));

            return addedDoctor.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _doctorRepository.DeleteAsync(id);
        }

        public List<DoctorDto> GetAll()
        {
            var doctors = _doctorRepository.GetAll();

            return _mapper.Map<List<DoctorDto>>(doctors.ToList());
        }

        public async Task<DoctorDto> GetAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task UpdateAsync(DoctorDto item)
        {
            await _doctorRepository.UpdateAsync(_mapper.Map<Doctor>(item));
        }
    }
}
