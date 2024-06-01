using Dapper.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Dapper.Repository
{
    public partial interface IRepository
    {
        public class CompanyRepository : IRepository<Company>
        {
            private readonly SqlConnection _sqlConnection;
            private readonly IConfiguration _config;

            public CompanyRepository(IConfiguration configuration)
            {
                this._config = configuration;
                _sqlConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
            public Company Add(Company entity)
            {
                var query =
                    @"INSERT INTO 
                    Companies(Name, Address, City, State, PostalCode) 
                    VALUES(@Name, @Address, @City, @State, @PostalCode);
                    SELECT CAST(SCOPE_IDENTITY() as int);";
                var id = _sqlConnection.Query<int>(query, new
                {
                    entity.Name,
                    entity.Address,
                    entity.City,
                    @State = entity.State,
                    entity.PostalCode,
                }).Single();
                entity.CompanyId = id;
                return entity;
            }

            public async Task<Company?> Find(int id)
            {
                var query = "SELECT * FROM COMPANIES WHERE CompanyId=@CompanyId;";
                return await _sqlConnection
                    .QuerySingleAsync<Company>(query, new { @CompanyId = id });
            }

            public async Task<IEnumerable<Company>> GetAll()
            {
                var query = "SELECT * FROM Companies;";
                var result = await _sqlConnection.QueryAsync<Company>(query);
                return result.ToList();
            }

            public void Remove(int id)
            {
                _sqlConnection
                    .Execute("Delete from Companies where CompanyId=@CompanyId",
                    new { @CompanyId = id });
            }

            public Company Update(Company entity)
            {
                var query = @"UPDATE Companies 
                                    SET Name = @Name, Address = @Address,
                                    City = @City, State = @State,
                                    PostalCode = @PostalCode
                                    WHERE CompanyId = @CompanyId;";
                _sqlConnection.Execute(query, entity);
                return entity;

            }
        }
    }
}
