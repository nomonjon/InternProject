using Education.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Api.Data.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .HasMaxLength(100);
        builder.Property(t => t.Gender)
            .HasDefaultValue(Gender.Unknown);

        builder.HasOne(t => t.City)
            .WithMany(c => c.Teachers)
            .HasForeignKey(t => t.CityId);

        builder.HasIndex(t => t.Name);
        builder.HasIndex(t => t.Gender);
    }
}
