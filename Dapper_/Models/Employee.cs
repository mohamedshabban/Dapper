using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using TableAttribute = Dapper.Contrib.Extensions.TableAttribute;

namespace Dapper_.Models
{
	[Table("Employees")]
    public class Employee
	{
		[Key]
		public int EmployeeId { get; set;}
		public string Name { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }
		public string Phone { get; set; }
        public int CompanyId { get; set; }
		[ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
    }
}