using Dapper.Data;
using Dapper.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapper.Repository
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

        public async Task<Company?> Find(int id)
        {
            return await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == id);
        }

        public void Remove(int id)
        {
            var company = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            if (company != null)
            {
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

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _context.Companies.ToListAsync();
        }
    }
}
