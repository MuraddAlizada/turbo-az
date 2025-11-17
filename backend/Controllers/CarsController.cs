using Microsoft.AspNetCore.Mvc;
using TurboAz.Api.Models;
using TurboAz.Api.Services;

namespace TurboAz.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetCars([FromQuery] CarFilterOptions? filters)
    {
        var cars = await _carService.GetCarsAsync(filters);
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCar(string id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return Ok(car);
    }

    [HttpGet("filter-options")]
    public async Task<ActionResult<FilterData>> GetFilterOptions()
    {
        var options = await _carService.GetFilterOptionsAsync();
        return Ok(options);
    }
}

