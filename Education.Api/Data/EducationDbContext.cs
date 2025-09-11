using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Data;

public class EducationDbContext(DbContextOptions<EducationDbContext> options) : DbContext(options)
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EducationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}