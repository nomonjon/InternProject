using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class StudentSubjectRepo(EducationDbContext dbContext) : IStudentSubjectRepo
{
    private readonly EducationDbContext dbContext = dbContext;

    public async Task<IEnumerable<Dtos.StudentSubject>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.StudentSubjects.AsNoTracking()
            .Include(ss => ss.Student)
            .Include(ss => ss.Subject)
            .Select(ss => ss.ToDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<Dtos.StudentSubject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.StudentSubjects.AsNoTracking()
            .Include(ss => ss.Student)
            .Include(ss => ss.Subject)
            .Select(ss => ss.ToDto())
            .FirstOrDefaultAsync(ss => ss.Id == id, cancellationToken);
    }

    public async Task<StudentSubject> AddAsync(Dtos.CreateUpdateStudentSubject dto, CancellationToken cancellationToken = default)
    {
        var entry = dto.ToModel();
        entry.CreatedDate = DateTime.UtcNow;
        entry.LastUpdatedDate = DateTime.UtcNow;

        dbContext.StudentSubjects.Add(entry);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }

    public async Task<StudentSubject?> UpdateAsync(Guid id, Dtos.CreateUpdateStudentSubject dto, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.StudentSubjects.FirstOrDefaultAsync(ss => ss.Id == id, cancellationToken);

        if (entry is null)
            return null;

        entry.StudentId = dto.StudentId;
        entry.SubjectId = dto.SubjectId;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.StudentSubjects.FirstOrDefaultAsync(ss => ss.Id == id, cancellationToken);

        if (entry is null)
            return false;

        entry.IsDeleted = true;
        entry.LastUpdatedDate = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}