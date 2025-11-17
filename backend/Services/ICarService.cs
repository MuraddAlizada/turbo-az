using TurboAz.Api.Models;

namespace TurboAz.Api.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetCarsAsync(CarFilterOptions? filters = null);
    Task<Car?> GetCarByIdAsync(string id);
    Task<FilterData> GetFilterOptionsAsync();
}

