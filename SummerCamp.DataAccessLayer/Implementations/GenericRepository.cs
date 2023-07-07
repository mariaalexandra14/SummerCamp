using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly SummerCampDbContext _context;

        public GenericRepository(SummerCampDbContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Save().GetAwaiter().GetResult();

            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save().GetAwaiter().GetResult();
        }

        public IList<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            Save().GetAwaiter().GetResult();

            return entity;
        }
    }
}
