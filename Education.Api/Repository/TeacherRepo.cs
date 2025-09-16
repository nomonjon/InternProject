using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class TeacherRepo(EducationDbContext _dbContext) : ITeacherRepo
{
    private EducationDbContext dbContext = _dbContext;
    public async Task<Teacher> AddAsync(Dtos.CreateUpdateTeacher teacher, CancellationToken cancellationToken = default)
    {
        var entry = teacher.ToModel();
        entry.CreatedDate = DateTime.UtcNow;
        entry.LastUpdatedDate = DateTime.UtcNow;

        dbContext.Teachers.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (entry is null)
            return false;

        entry.IsDeleted = true;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IEnumerable<Dtos.Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Teachers.AsNoTracking()
            .Include(t => t.City)
            .Select(t => t.ToDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<Dtos.Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Teachers.AsNoTracking()
            .Include(t => t.City)
            .Select(t => t.ToDto())
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<Teacher?> UpdateAsync(Guid id, Dtos.CreateUpdateTeacher teacher, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Teachers.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (entry is null)
            return null;


        entry.Name = teacher.Name;
        entry.CityId = teacher.CityId;
        entry.BirthDate = teacher.BirthDate;
        entry.Gender = teacher.Gender switch
            {
                Dtos.GenderDto.Male => Models.Gender.Male,
                Dtos.GenderDto.Female => Models.Gender.Female,
                _ => Models.Gender.Unknown
            };

        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }
}
