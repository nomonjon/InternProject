using Education.Api.Interfaces;
using Education.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DepartmentController(
    IDepartmentRepo repo,
    ILogger<DepartmentController> logger) : ControllerBase
{
    private readonly IDepartmentRepo repo = repo;
    private readonly ILogger<DepartmentController> logger = logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var departments = await repo.GetAllAsync(cancellationToken);
            return Ok(departments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting all departments");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var department = await repo.GetByIdAsync(id, cancellationToken);
            if (department is null)
                return NotFound();

            return Ok(department);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while getting department with id {DepartmentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Department>> Create(Dtos.CreateUpdateDepartment departmentDto, CancellationToken cancellationToken)
    {
        try
        {
            var department = await repo.AddAsync(departmentDto, cancellationToken);
            return Ok(department);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while creating department");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Department>> Update(Guid id, Dtos.CreateUpdateDepartment dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await repo.UpdateAsync(id, dto, cancellationToken);
            if (updated is null)
                return NotFound();

            return Ok(updated);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while updating department with id {DepartmentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var deleted = await repo.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while deleting department with id {DepartmentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}