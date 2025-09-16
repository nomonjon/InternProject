using Education.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TeacherController(
    ITeacherRepo _teacherRepo,
    ILogger<TeacherController> _logger) : ControllerBase
{
    private readonly ITeacherRepo teacherRepo = _teacherRepo;
    private readonly ILogger<TeacherController> logger = _logger;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching all teachers");
            var teachers = await teacherRepo.GetAllAsync(cancellationToken);
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all teachers");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching teacher with id {Id}", id);
            var teacher = await teacherRepo.GetByIdAsync(id, cancellationToken);
            if (teacher is null)
                return NotFound();

            return Ok(teacher);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching teacher with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Dtos.CreateUpdateTeacher dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await teacherRepo.AddAsync(dto, cancellationToken);
            logger.LogInformation("Teacher created with id {Id}", created.Id);
            return Ok(created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating teacher");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Dtos.CreateUpdateTeacher dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await teacherRepo.UpdateAsync(id, dto, cancellationToken);
            if (updated is null)
                return NotFound();

            logger.LogInformation("Teacher with id {Id} updated successfully", id);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating teacher with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await teacherRepo.DeleteAsync(id, cancellationToken);
            logger.LogInformation("Teacher with id {Id} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting teacher with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}