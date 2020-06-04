using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Clinic.BLL.Dto;
using Clinic.BLL.Interfaces;
using Clinic.DAL.Interfaces;

namespace Clinic.BLL.Services
{
    internal class PatientService : IPatientService
    {
        private readonly IRepository<DAL.Domain.Patient> _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IRepository<DAL.Domain.Patient> patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<Patient> AddAsync(Patient item)
        {
            if (item is null)
            {
                throw new ArgumentNullException();
            }

            var addedPatient = await _patientRepository.AddAsync(_mapper.Map<DAL.Domain.Patient>(item));

            return _mapper.Map<Patient>(addedPatient);
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _patientRepository.GetAsync(id);

            if (patient is null)
            {
                throw new ArgumentException($"Patient with specified id = {patient.Id} not exists");
            }

            await _patientRepository.DeleteAsync(patient.Id);
        }

        public List<Patient> GetAll()
        {
            var patients = _patientRepository.GetAll().ToList();

            return _mapper.Map<List<Patient>>(patients);
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = await _patientRepository.GetAsync(id);

            if (patient is null)
            {
                throw new ArgumentException($"Patient with specified id = {id} not exists");
            }

            return _mapper.Map<Patient>(patient);
        }

        public async Task<Patient> UpdateAsync(Patient item)
        {
            if (item is null)
            {
                throw new ArgumentNullException();
            }

            var patient = await _patientRepository.GetAsync(item.Id);

            if (patient is null)
            {
                throw new ArgumentException($"Doctor with specified id = {patient.Id} not exists");
            }

            var updatedPatient = await _patientRepository.UpdateAsync(_mapper.Map<Clinic.DAL.Domain.Patient>(item));

            return _mapper.Map<Patient>(updatedPatient);
        }
    }
}
