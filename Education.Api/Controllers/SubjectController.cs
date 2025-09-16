using Education.Api.Dtos;
using Education.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController(
    ISubjectRepo _subjectRepo,
    ILogger<SubjectController> _logger) : ControllerBase
{
    private readonly ISubjectRepo subjectRepo = _subjectRepo;
    private readonly ILogger<SubjectController> logger = _logger;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching all subjects");
            var subjects = await subjectRepo.GetAllAsync(cancellationToken);
            return Ok(subjects);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all subjects");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching subject with id {Id}", id);
            var subject = await subjectRepo.GetByIdAsync(id, cancellationToken);
            if (subject is null)
                return NotFound();

            return Ok(subject);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching subject with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUpdateSubject subject, CancellationToken cancellationToken)
    {
        try
        {
            var created = await subjectRepo.AddAsync(subject, cancellationToken);
            logger.LogInformation("Subject created with id {Id}", created.Id);
            return Ok(created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating subject");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CreateUpdateSubject subject, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await subjectRepo.UpdateAsync(id, subject, cancellationToken);
            if (updated is null)
                return NotFound();

            logger.LogInformation("Subject with id {Id} updated successfully", id);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating subject with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await subjectRepo.DeleteAsync(id, cancellationToken);
            logger.LogInformation("Subject with id {Id} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting subject with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
