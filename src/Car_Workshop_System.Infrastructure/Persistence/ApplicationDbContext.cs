using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Car_Workshop_System.Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car_Workshop_System.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<WorkOrder> WorkOrders { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<TechnicianAssignment> TechnicianAssignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new TechnicianAssignmentConfiguration());
			modelBuilder.ApplyConfiguration(new NoteConfiguration());
		}
	}
}
