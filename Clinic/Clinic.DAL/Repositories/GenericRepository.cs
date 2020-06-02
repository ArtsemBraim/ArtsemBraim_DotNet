using System.Linq;
using System.Threading.Tasks;
using Clinic.DAL.Domain;
using Clinic.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic.DAL.Repositories
{
    internal class GenericRepository<T> : IRepository<T>
        where T : Entity
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T item)
        {
            var added = await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();

            return added.Entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<T> UpdateAsync(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
