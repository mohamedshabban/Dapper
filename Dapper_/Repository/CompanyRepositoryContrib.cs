using Dapper;
using System.Net;
using System.Data;
using Dapper_.Data;
using Dapper_.Models;
using System.Data.Common;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;

namespace Dapper_.Repository
{
	public partial interface IRepository
	{
		public class CompanyRepositoryContrib : IRepository<Company>
		{
			private readonly SqlConnection _db;
			private readonly IConfiguration _config;

			public CompanyRepositoryContrib(IConfiguration configuration)
			{
				this._config = configuration;
				_db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			}
			public Company Add(Company entity)
			{
				var x=_db.Insert(entity);
				entity.CompanyId = (int)x;
				return entity;	
			}

			public async Task<Company> Find(int id)
			{
				return _db.Get<Company>(id);
			}

			public List<Company> GetAll()
			{
				return _db.GetAll<Company>().ToList();
			}

			public void Remove(int id)
			{
				_db.Delete(new Company {CompanyId=id});
			}

			public Company Update(Company entity)
			{
				_db.Update(entity);
				return entity;
            }
		}
	}
}
