using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Workshop_System.Infrastructure.Persistence.Configuration
{
	public class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
	{
		public void Configure(EntityTypeBuilder<Technician> builder)
		{
			builder.HasKey(t => t.Id);

			builder
				.HasOne<ApplicationUser>()
				.WithOne(u => u.Technician)
				.HasForeignKey<Technician>(t => t.UserId)
				.HasPrincipalKey<ApplicationUser>(u => u.Id);
		}
	}
}
