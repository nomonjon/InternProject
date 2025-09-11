using Education.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Education.Api.Data.Configurations;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name)
            .HasMaxLength(100);

        builder.HasMany(s => s.StudentSubjects)
            .WithOne(ss => ss.Subject)
            .HasForeignKey(ss => ss.SubjectId);

        builder.HasMany(s => s.TeacherSubjects)
            .WithOne(ts => ts.Subject)
            .HasForeignKey(ts => ts.SubjectId);

        builder.HasIndex(s => s.Name);
    }
}
