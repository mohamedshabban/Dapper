using Dapper_.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dapper_.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Fluent API configuration while creating DB
			modelBuilder.Entity<Company>().Ignore(c=>c.Employees);
			modelBuilder.Entity<Employee>()
				.HasOne(c => c.Company)
				.WithMany(c => c.Employees)
				.HasForeignKey(c => c.CompanyId);

            modelBuilder.Entity<Employee>(b =>
            {
                b.HasKey(e => e.EmployeeId);
                b.Property(e => e.EmployeeId)
				.ValueGeneratedOnAdd();
            });
        }
		public virtual DbSet<Company> Companies { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
	}
}
