using Car_Workshop_System.Domain.Entities;
using Car_Workshop_System.Infrastructure.Auth.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car_Workshop_System.Infrastructure.Persistence.Configuration
{
	public class NoteConfiguration : IEntityTypeConfiguration<Note>
	{
		public void Configure(EntityTypeBuilder<Note> builder)
		{
			builder.HasKey(n => n.Id);

			builder
				.HasOne<ApplicationUser>()
				.WithMany(u => u.Notes)
				.HasForeignKey(n => n.TechnicianId);
		}
	}
}
