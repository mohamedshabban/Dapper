using Dapper.Data;
using Dapper.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Dapper.Repository.IRepository;
using Dapper.Models;

namespace Dapper
{
	public class Program
	{

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IRepository<Company>, CompanyRepositorySP>();
			builder.Services.AddScoped<IRepository<Company>, CompanyRepositoryContrib>();
			builder.Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}