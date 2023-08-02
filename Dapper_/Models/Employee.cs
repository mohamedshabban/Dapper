using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dapper_.Models
{
    public class Employee
	{
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