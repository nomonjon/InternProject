using Education.Api.Dtos;
using Education.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilterController(
    IFilterRepo _filterRepo,
    ILogger<FilterController> _logger) : ControllerBase
{
    private readonly IFilterRepo filterRepo = _filterRepo;
    private readonly ILogger<FilterController> logger = _logger;

    [HttpPost("students")]
    public async Task<IActionResult> FilterStudentsAsync([FromBody] StudentFilterDto dto, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Filtering students");
            var students = await filterRepo.FilterStudentsAsync(dto, cancellationToken);
            return Ok(students);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error filtering students");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("teachers")]
    public async Task<IActionResult> FilterTeachersAsync([FromBody] TeacherFilterDto dto, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Filtering teachers");
            var teachers = await filterRepo.FilterTeachersAsync(dto, cancellationToken);
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error filtering teachers");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("students/top10/{subjectId}")]
    public async Task<IActionResult> GetTop10StudentsBySubjectAsync(Guid subjectId, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching top 10 students by subject {SubjectId}", subjectId);
            var students = await filterRepo.GetTop10StudentsBySubjectAsync(subjectId, cancellationToken);
            return Ok(students);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching top 10 students by subject {SubjectId}", subjectId);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("teachers/top10/{subjectId}")]
    public async Task<IActionResult> GetTop10TeachersByTopStudentsAsync(Guid subjectId, [FromQuery] bool best, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Fetching top 10 teachers by top students for subject {SubjectId}", subjectId);
            var teachers = await filterRepo.GetTop10TeachersByTopStudentsAsync(subjectId, best, cancellationToken);
            return Ok(teachers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching top 10 teachers by top students for subject {SubjectId}", subjectId);
            return StatusCode(500, "Internal server error");
        }
    }
}
