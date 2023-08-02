using Dapper_.Models;

namespace Dapper_.Repository
{
	public partial interface IRepository
	{
		public interface IRepository<T>
		{
			Task<T> Find(int id);
			T Add(T entity);
			T Update(T entity);
			void Remove(int id);
			List<T> GetAll();
		}
	}
}
