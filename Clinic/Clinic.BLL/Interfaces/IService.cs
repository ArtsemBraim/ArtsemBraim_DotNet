using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.BLL.Interfaces
{
    public interface IService<T>
    {
        List<T> GetAll();

        Task<T> GetAsync(int id);

        Task<int> AddAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(int id);
    }
}
