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
    internal class DoctorService : IDoctorService
    {
        private readonly IRepository<DAL.Domain.Doctor> _doctorRepository;
        private readonly IRepository<DAL.Domain.Patient> _patientRepository;
        private readonly IRepository<DAL.Domain.Reception> _receptionRepository;

        private readonly IMapper _mapper;

        public DoctorService(
            IRepository<DAL.Domain.Doctor> doctorRepository,
            IRepository<DAL.Domain.Patient> patientRepository,
            IRepository<DAL.Domain.Reception> receptionRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _receptionRepository = receptionRepository;
            _mapper = mapper;
        }

        public List<Doctor> GetAll()
        {
            var doctors = _doctorRepository.GetAll().ToList();

            return _mapper.Map<List<Doctor>>(doctors);
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor is null)
            {
                throw new ArgumentException($"Doctor with specified id = {id} not exists");
            }

            return _mapper.Map<Doctor>(doctor);
        }

        public async Task<Doctor> AddAsync(Doctor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException();
            }

            var addedDoctor = await _doctorRepository.AddAsync(_mapper.Map<DAL.Domain.Doctor>(item));

            return _mapper.Map<Doctor>(addedDoctor);
        }

        public async Task<Doctor> UpdateAsync(Doctor item)
        {
            if (item is null)
            {
                throw new ArgumentNullException();
            }

            var doctor = await _doctorRepository.GetAsync(item.Id);

            if (doctor is null)
            {
                throw new ArgumentException($"Doctor with specified id = {doctor.Id} not exists");
            }

            var updatedDoctor = await _doctorRepository.UpdateAsync(_mapper.Map<Clinic.DAL.Domain.Doctor>(item));

            return _mapper.Map<Doctor>(updatedDoctor);
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor is null)
            {
                throw new ArgumentException($"Doctor with specified id = {doctor.Id} not exists");
            }

            await _doctorRepository.DeleteAsync(doctor.Id);
        }

        public async Task AddReception(Reception reception)
        {
            var doctor = await _doctorRepository.GetAsync(reception.DoctorId);
            if (doctor is null)
            {
                throw new ArgumentException($"Doctor with specified id = {doctor.Id} not exists");
            }

            var patient = await _patientRepository.GetAsync(reception.PatientId);
            if (patient is null)
            {
                throw new ArgumentException($"Doctor with specified id = {doctor.Id} not exists");
            }

            if (reception.ReceptionTime < DateTime.Now)
            {
                throw new ArgumentException("Reception time can not be in the past");
            }

            await _receptionRepository.AddAsync(_mapper.Map<DAL.Domain.Reception>(reception));
        }

        public async Task<Doctor> GetByIdWithPatients(int id)
        {
            var doctor = await _doctorRepository.GetAsync(id);

            if (doctor is null)
            {
                throw new ArgumentException($"Doctor with specified id = {doctor.Id} not exists");
            }

            var receptions = _receptionRepository.GetAll().Where(r => r.DoctorId == id && r.ReceptionTime > DateTime.Now).ToList();

            var receptionsDto = _mapper.Map<List<Reception>>(receptions);
            foreach (var item in receptionsDto)
            {
                var patient = await _patientRepository.GetAsync(item.PatientId);
                item.Patient = _mapper.Map<Patient>(patient);
            }

            var doctorWithPatients = _mapper.Map<Doctor>(doctor);
            doctorWithPatients.Receptions = receptionsDto;

            return doctorWithPatients;
        }
    }
}
