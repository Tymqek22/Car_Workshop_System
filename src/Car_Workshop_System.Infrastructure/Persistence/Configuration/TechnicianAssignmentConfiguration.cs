using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Workshop_System.Infrastructure.Persistence.Configuration
{
	public class TechnicianAssignmentConfiguration : IEntityTypeConfiguration<TechnicianAssignment>
	{
		public void Configure(EntityTypeBuilder<TechnicianAssignment> builder)
		{
			builder.HasKey(ta => new { ta.TechnicianId, ta.WorkOrderId});

			builder
				.HasOne(ta => ta.WorkOrder)
				.WithMany(wo => wo.TechnicianAssignments)
				.HasForeignKey(ta => ta.WorkOrderId);

			builder
				.HasOne<ApplicationUser>()
				.WithMany(u => u.TechnicianAssignments)
				.HasForeignKey(ta => ta.TechnicianId);

			builder.ToTable("TechnicianAssignments");
		}
	}
}
