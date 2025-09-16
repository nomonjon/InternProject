using Education.Api.Models;

namespace Education.Api.Interfaces;

public interface ISubjectRepo
{
    Task<IEnumerable<Subject>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Subject?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Subject> AddAsync(Dtos.CreateUpdateSubject subject, CancellationToken cancellationToken = default);
    Task<Subject?> UpdateAsync(Guid id, Dtos.CreateUpdateSubject subject, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
