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
    internal class PatientService : IService<PatientDto>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IRepository<Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(PatientDto item)
        {
            var addedPatient = await _patientRepository.AddAsync(_mapper.Map<Patient>(item));

            return addedPatient.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        public List<PatientDto> GetAll()
        {
            var patients = _patientRepository.GetAll();

            return _mapper.Map<List<PatientDto>>(patients.ToList());
        }

        public async Task<PatientDto> GetAsync(int id)
        {
            var patient = await _patientRepository.GetAsync(id);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task UpdateAsync(PatientDto item)
        {
            await _patientRepository.UpdateAsync(_mapper.Map<Patient>(item));
        }
    }
}
