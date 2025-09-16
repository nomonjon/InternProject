using Education.Api.Data;
using Education.Api.Interfaces;
using Education.Api.Mappers;
using Education.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Education.Api.Repository;

public class CityRepo(
    EducationDbContext _dbContext) : ICityRepo
{
    private readonly EducationDbContext dbContext = _dbContext;

    public async Task<City> AddAsync(Dtos.CreateUpdateCity city, CancellationToken cancellationToken = default)
    {
        var modelcity = city.ToModel();
        modelcity.CreatedDate = DateTime.UtcNow;
        modelcity.LastUpdatedDate = DateTime.UtcNow;

        var entry = dbContext.Cities.Add(modelcity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var city = await dbContext.Cities
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (city is null)
            return false;

        city.IsDeleted = true;
        city.LastUpdatedDate = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Cities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Cities
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<City?> UpdateAsync(Guid id,Dtos.CreateUpdateCity city, CancellationToken cancellationToken = default)
    {
        var entry = await dbContext.Cities
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (entry is null)
            return null;

        entry.Name = city.Name;
        entry.LastUpdatedDate = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return entry;
    }
}
