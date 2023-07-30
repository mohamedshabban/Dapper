using Dapper_.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Dapper_.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Company> Companies { get; set; }
	}
}
