using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface ICityRepo
{
    Task<IEnumerable<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<City> AddAsync(Dtos.CreateUpdateCity city, CancellationToken cancellationToken = default);
    Task<City?> UpdateAsync(Guid id, Dtos.CreateUpdateCity city, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
