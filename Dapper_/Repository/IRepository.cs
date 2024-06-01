using Dapper.Models;

namespace Dapper.Repository
{
    public interface IRepository<T>
    {
        Task<T?> Find(int id);
        T Add(T entity);
        Company Update(T entity);
        void Remove(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
