using Dapper.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dapper.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Company> Companies { get; set; }
	}
}
