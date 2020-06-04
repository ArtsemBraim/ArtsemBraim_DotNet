using Clinic.BLL.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.BLL.Interfaces
{
    public interface IPatientService
    {
        List<Patient> GetAll();

        Task<Patient> GetByIdAsync(int id);

        Task<Patient> AddAsync(Patient item);

        Task<Patient> UpdateAsync(Patient item);

        Task<Patient> DeleteAsync(int id);
    }
}
