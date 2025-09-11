using Education.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Api.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .HasMaxLength(100);
        builder.Property(s => s.Gender)
            .HasDefaultValue(Gender.Unknown);

        builder.HasOne(s => s.City)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CityId);

        builder.HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId);

        builder.HasMany(s => s.StudentSubjects)
            .WithOne(ss => ss.Student)
            .HasForeignKey(ss => ss.StudentId);

        builder.HasIndex(s => s.Name);
        builder.HasIndex(s => s.Gender);
        builder.HasIndex(s => s.CurrentGradeLevel);
    }
}
