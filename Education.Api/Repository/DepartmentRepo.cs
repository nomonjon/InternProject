using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class DepartmentRepo(
    EducationDbContext _dbContext) : IDepartmentRepo
{
    private readonly EducationDbContext dbContext = _dbContext;

    public async Task<Department> AddAsync(Dtos.CreateUpdateDepartment department, CancellationToken cancellationToken = default)
    {
        var model = department.ToModel();
        model.CreatedDate = DateTime.UtcNow;
        model.LastUpdatedDate = DateTime.UtcNow;

        var entry = dbContext.Departments.Add(model);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var department = await dbContext.Departments
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (department is null)
            return false;

        department.IsDeleted = true;
        department.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Departments
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<Department?> UpdateAsync(Guid id, Dtos.CreateUpdateDepartment department, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Departments
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (entry is null)
            return null;

        entry.Name = department.Name;
        entry.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }
}
