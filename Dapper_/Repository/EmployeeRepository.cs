using Dapper;
using System.Net;
using Dapper_.Data;
using Dapper_.Models;
using System.Data.Common;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Dapper_.Repository
{
	public partial interface IRepository
	{
		public class EmployeeRepository : IRepository<Employee>
		{
			private readonly SqlConnection _sqlConnection;
			private readonly IConfiguration _config;

			public EmployeeRepository(IConfiguration configuration)
			{
				this._config = configuration;
				_sqlConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			}

            public Employee Add(Employee entity)
            {
                var query = @"INSERT INTO Employees (Name, Title, Email, Phone, CompanyId)
                                VALUES(@Name, @Title, @Email, @Phone, @CompanyId);
                                SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = _sqlConnection.Query<int>(query, entity).Single();
                entity.EmployeeId = id;

                return entity;
            }


            public async Task<Employee> Find(int id)
            {
                var query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId;";
                return _sqlConnection.Query<Employee>(query, new { @EmployeeId = id }).Single();
            }

            public List<Employee> GetAll()
            {
                var query = "SELECT * FROM Employees";
                return _sqlConnection.Query<Employee>(query).ToList();
            }

            public void Remove(int id)
            {
                _sqlConnection.Execute("DELETE FROM Employees WHERE EmployeeId = @EmployeeId", new { @EmployeeId = id });
            }

            public Employee Update(Employee entity)
            {
                var query = @"UPDATE Employees SET Name = @Name,  
                    Title = @Title, Email = @Email, Phone = @Phone, 
                    CompanyId = @CompanyId
                    WHERE EmployeeId = @EmployeeId";
                _sqlConnection.Execute(query, entity);
                return entity;

            }

        }
    }
}
