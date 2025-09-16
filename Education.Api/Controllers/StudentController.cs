using Education.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(
    IStudentRepo _studentRepo,
    ILogger<StudentController> _logger) : ControllerBase
{
    private readonly IStudentRepo studentRepo = _studentRepo;
    private readonly ILogger<StudentController> logger = _logger;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching all students");
            var students = await studentRepo.GetAllAsync(cancellationToken);
            return Ok(students);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching all students");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching student with id {Id}", id);
            var student = await studentRepo.GetByIdAsync(id, cancellationToken);
            if (student is null)
                return NotFound();

            return Ok(student);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching student with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Dtos.CreateUpdateStudent dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await studentRepo.AddAsync(dto, cancellationToken);
            logger.LogInformation("Student created with id {Id}", created.Id);
            return Ok(created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating student");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Dtos.CreateUpdateStudent dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await studentRepo.UpdateAsync(id, dto, cancellationToken);
            if (updated is null)
                return NotFound();

            logger.LogInformation("Student with id {Id} updated successfully", id);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating student with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await studentRepo.DeleteAsync(id, cancellationToken);
            logger.LogInformation("Student with id {Id} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting student with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
