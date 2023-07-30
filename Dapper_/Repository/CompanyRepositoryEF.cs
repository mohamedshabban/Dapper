using Dapper_.Data;
using Dapper_.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapper_.Repository
{
	public partial interface IRepository
	{
		public class CompanyRepositoryEF : IRepository<Company>
		{
			private readonly ApplicationDbContext _context;

			public CompanyRepositoryEF(ApplicationDbContext context)
			{
				_context = context;
			}
			public Company Add(Company entity)
			{
				_context.Companies.Add(entity);
				_context.SaveChanges();
				return entity;
			}

			public async Task<Company> Find(int id)
			{
				return await _context.Companies
					.FirstOrDefaultAsync(c=>c.CompanyId==id);
			}

			public List<Company> GetAll()
			{
				return  _context.Companies.ToList();
			}

			public void Remove(int id)
			{
				var company=_context.Companies.FirstOrDefault(c=>c.CompanyId==id);
				if(company!=null) {
					_context.Companies.Remove(company);
					_context.SaveChanges();
				}
				return;
			}

			public Company Update(Company entity)
			{
				_context.Companies.Update(entity);
				_context.SaveChanges();
				return entity;
			}
		}
	}
}
