using Dapper;
using System.Net;
using System.Data;
using Dapper_.Data;
using Dapper_.Models;
using System.Data.Common;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;

namespace Dapper_.Repository
{
	public partial interface IRepository
	{
		public class CompanyRepositorySP : IRepository<Company>
		{
			private readonly SqlConnection _db;
			private readonly IConfiguration _config;

			public CompanyRepositorySP(IConfiguration configuration)
			{
				this._config = configuration;
				_db = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			}
			public Company Add(Company entity)
			{
				//Dyanmic Parameters
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@Name", entity.Name);
				parameters.Add("@Address", entity.Address);
				parameters.Add("@City", entity.City);
				parameters.Add("@State", entity.State);
				parameters.Add("@PostalCode", entity.PostalCode);
				parameters.Add("@CompanyId", 0, DbType.Int32,direction: ParameterDirection.Output);
				_db.Execute("usp_AddCompany",parameters,commandType:CommandType.StoredProcedure);
				entity.CompanyId = parameters.Get<int>("@CompanyId"); 
				return entity;
			}

			public async Task<Company> Find(int id)
			{
				return _db.Query<Company>("usp_GetCompany",  new {CompanyId=id },commandType: CommandType.StoredProcedure).Single();
			}

			public List<Company> GetAll()
			{
				return _db.Query<Company>("usp_GetALLCompany",commandType:CommandType.StoredProcedure).ToList();
			}

			public void Remove(int id)
			{
				_db.Execute("usp_RemoveCompany", new {@CompanyId=id},commandType:CommandType.StoredProcedure);
			}

			public Company Update(Company entity)
			{
                //Dyanmic Parameters
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@Address", entity.Address);
                parameters.Add("@City", entity.City);
                parameters.Add("@State", entity.State);
                parameters.Add("@PostalCode", entity.PostalCode);
                parameters.Add("@CompanyId", entity.CompanyId, DbType.Int32);
                _db.Execute("usp_AddCompany", parameters, commandType: CommandType.StoredProcedure);
                return entity;

            }
		}
	}
}
