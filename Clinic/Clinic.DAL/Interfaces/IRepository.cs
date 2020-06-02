﻿using System.Linq;
using System.Threading.Tasks;

namespace Clinic.DAL.Interfaces
{
    public interface IRepository<T> 
        where T : class
    {
        IQueryable<T> GetAll();

        Task<T> GetAsync(int id);

        Task<T> AddAsync(T item);

        Task<T> UpdateAsync(T item);

        Task<T> DeleteAsync(int id);
    }
}
