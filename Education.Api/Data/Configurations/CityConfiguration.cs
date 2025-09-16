using Education.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Api.Data.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.HasMany(c => c.Students)
            .WithOne(s => s.City)
            .HasForeignKey(s => s.CityId);

        builder.HasMany(c => c.Teachers)
            .WithOne(t => t.City)
            .HasForeignKey(t => t.CityId);

        builder.HasIndex(c => new { c.Name, c.IsDeleted }).IsUnique();
    }
}
