using Clinic.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.BLL.Interfaces
{
    public interface IDoctorService
    {
        List<Doctor> GetAll();

        Task<Doctor> GetByIdAsync(int id);

        Task<Doctor> AddAsync(Doctor item);

        Task<Doctor> UpdateAsync(Doctor item);

        Task DeleteAsync(int id);

        Task AddReception(Reception reception);

        Task<Doctor> GetByIdWithPatients(int id);
    }
}
