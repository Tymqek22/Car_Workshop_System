using Car_Workshop_System.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Car_Workshop_System.Infrastructure.Auth.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Specialization { get; set; }

		public ICollection<Note>? Notes { get; set; }
		public ICollection<TechnicianAssignment>? TechnicianAssignments { get; set; }
	}
}
