using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IList<T> GetAll();
        T? GetById(int id);
        T Update(T entity);
        void Delete(T entity);
        IList<T> Get(Expression<Func<T, bool>> expression);
        T Add(T entity);
        Task Save();
    }
}