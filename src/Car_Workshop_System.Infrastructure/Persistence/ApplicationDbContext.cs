using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Car_Workshop_System.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<WorkOrder> WorkOrders { get; set; }
		public DbSet<Technician> Technicians { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<TechnicianAssignment> TechnicianAssignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new TechnicianAssignmentConfiguration());
		}
	}
}
