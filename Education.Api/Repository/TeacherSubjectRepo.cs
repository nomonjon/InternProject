using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class TeacherSubjectRepo(EducationDbContext dbContext) : ITeacherSubjectRepo
{
    private readonly EducationDbContext dbContext = dbContext;

    public async Task<IEnumerable<Dtos.TeacherSubject>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.TeacherSubjects.AsNoTracking()
            .Include(ts => ts.Teacher)
            .Include(ts => ts.Subject)
            .Select(ts => ts.ToDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<Dtos.TeacherSubject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.TeacherSubjects.AsNoTracking()
            .Include(ts => ts.Teacher)
            .Include(ts => ts.Subject)
            .Select(ts => ts.ToDto())
            .FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);
    }

    public async Task<TeacherSubject> AddAsync(Dtos.CreateUpdateTeacherSubject dto, CancellationToken cancellationToken = default)
    {
        var entry = dto.ToModel();
        entry.Id = Guid.NewGuid();
        entry.CreatedDate = DateTime.UtcNow;
        entry.LastUpdatedDate = DateTime.UtcNow;

        dbContext.TeacherSubjects.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }

    public async Task<TeacherSubject?> UpdateAsync(Guid id, Dtos.CreateUpdateTeacherSubject dto, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.TeacherSubjects.FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);

        if (entry is null)
            return null;

        entry.TeacherId = dto.TeacherId;
        entry.SubjectId = dto.SubjectId;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.TeacherSubjects.FirstOrDefaultAsync(ts => ts.Id == id, cancellationToken);

        if (entry is null)
            return false;

        entry.IsDeleted = true;
        entry.LastUpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
