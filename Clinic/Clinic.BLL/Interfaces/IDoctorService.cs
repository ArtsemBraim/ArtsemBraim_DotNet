using Clinic.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.BLL.Interfaces
{
    interface IDoctorService
    {
        List<Doctor> GetAll();

        Task<Doctor> GetByIdAsync(int id);

        Task<Doctor> AddAsync(Doctor item);

        Task<Doctor> UpdateAsync(Doctor item);

        Task<Doctor> DeleteAsync(int id);

        Task AddPatient(Reception reception);

        Task<Doctor> GetByIdWithPatients(int id);
    }
}
