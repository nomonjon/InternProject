using System.Text;
using Education.Api.Interfaces;
using Education.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController(ICityRepo _cityRepo,
    ILogger<CityController> logger) : ControllerBase
{
    private readonly ICityRepo cityRepo = _cityRepo;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Dtos.CreateUpdateCity dtocity,
        CancellationToken cancellationToken)
    {
        try
        {
            var createdCity = await cityRepo.AddAsync(dtocity, cancellationToken);
            logger.LogInformation("City created with name {Name}", dtocity.Name);
            return Ok(createdCity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while creating city");
            return StatusCode(500, "An error occurred while creating the city.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Loading all cities");
            var cities = await cityRepo.GetAllAsync(cancellationToken);

            return Ok(cities);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to load cities");
            return StatusCode(500, "An error occurred while loading cities.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Loading city with id {Id}", id);
            var city = await cityRepo.GetByIdAsync(id, cancellationToken);
            if (city is null)
                return NotFound();

            return Ok(city);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while loading city with id {Id}", id);
            return StatusCode(500, "An error occurred while loading the city.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await cityRepo.DeleteAsync(id, cancellationToken);
            logger.LogInformation("City with id {Id} deleted successfully.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting city with id {Id}", id);
            return StatusCode(500, "An error occurred while deleting the city.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] Dtos.CreateUpdateCity city,
        CancellationToken cancellationToken)
    {
        try
        {
            var updatedCity = await cityRepo.UpdateAsync(id, city, cancellationToken);

            if (updatedCity is null)
                return NotFound($"City with id {id} not found.");
            

            logger.LogInformation("City with id {Id} updated successfully.", id);
            return Ok(updatedCity);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating city with id {Id}", id);
            return StatusCode(500, "An error occurred while updating the city.");
        }
    }

}
