using Education.Api.Data;
using Education.Api.Dtos;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class StudentRepo(EducationDbContext _dbContext) : IStudentRepo
{
    private EducationDbContext dbContext = _dbContext;
    public async Task<Models.Student> AddAsync(CreateUpdateStudent student, CancellationToken cancellationToken = default)
    {
        var entry = student.ToModel();
        entry.CreatedDate = DateTime.UtcNow;
        entry.LastUpdatedDate = DateTime.UtcNow;

        dbContext.Students.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (entry is null)
            return false;

        entry.IsDeleted = true;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Dtos.Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Students.AsNoTracking()
            .Include(s => s.City)
            .Include(s => s.Department)
            .Select(s => s.ToDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<Dtos.Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Students.AsNoTracking()
            .Include(s => s.City)
            .Include(s => s.Department)
            .Select(s => s.ToDto())
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Models.Student?> UpdateAsync(Guid id, CreateUpdateStudent student, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (entry is null)
            return null;


        entry.Name = student.Name;
        entry.CityId = student.CityId;
        entry.DepartmentId = student.DepartmentId;
        entry.BirthDate = student.BirthDate;
        entry.Gender = student.Gender switch
            {
                Dtos.GenderDto.Male => Models.Gender.Male,
                Dtos.GenderDto.Female => Models.Gender.Female,
                _ => Models.Gender.Unknown
            };
        entry.CurrentGradeLevel = student.CurrentGradeLevel;

        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }
}
