using Education.Api.Dtos;
using Education.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentSubjectController(
    IStudentSubjectRepo studentSubjectRepo,
    ILogger<StudentSubjectController> logger) : ControllerBase
{
    private readonly IStudentSubjectRepo repo = studentSubjectRepo;
    private readonly ILogger<StudentSubjectController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching all student-subject relations");
            var result = await repo.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching student-subject relations");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Fetching student-subject relation with id {Id}", id);
            var result = await repo.GetByIdAsync(id, cancellationToken);
            if (result is null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching student-subject relation with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUpdateStudentSubject dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await repo.AddAsync(dto, cancellationToken);
            _logger.LogInformation("Student-subject relation created with id {Id}", created.Id);
            return Ok(created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating student-subject relation");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateUpdateStudentSubject dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await repo.UpdateAsync(id, dto, cancellationToken);
            if (updated is null)
                return NotFound();

            _logger.LogInformation("Student-subject relation with id {Id} updated successfully", id);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating student-subject relation with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await repo.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            _logger.LogInformation("Student-subject relation with id {Id} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting student-subject relation with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
