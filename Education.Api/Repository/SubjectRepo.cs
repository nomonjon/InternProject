using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class SubjectRepo(
    EducationDbContext _dbContext) : ISubjectRepo
{
    private readonly EducationDbContext dbContext = _dbContext;

    public async Task<Subject> AddAsync(Dtos.CreateUpdateSubject subject, CancellationToken cancellationToken = default)
    {
        var model = subject.ToModel();
        model.CreatedDate = DateTime.UtcNow;
        model.LastUpdatedDate = DateTime.UtcNow;

        var entry = dbContext.Subjects.Add(model);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var subject = await dbContext.Subjects
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (subject is null)
            return false;

        subject.IsDeleted = true;
        subject.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Subjects
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Subjects
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Subject?> UpdateAsync(Guid id, Dtos.CreateUpdateSubject subject, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Subjects
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (entry is null)
            return null;

        entry.Name = subject.Name;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }
}
