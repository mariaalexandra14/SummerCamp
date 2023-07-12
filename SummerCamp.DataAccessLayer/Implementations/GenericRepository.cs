using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Implementations
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected readonly SummerCampDbContext context;

        public GenericRepository(SummerCampDbContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            context.Set<T>().Add(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IList<T> Get(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression).ToList();
        }

        public IList<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public T Update(T entity)
        {
            context.Update(entity);

            return entity;
        }
    }
}
