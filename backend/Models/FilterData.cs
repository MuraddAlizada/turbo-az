namespace TurboAz.Api.Models;

public class FilterData
{
    public List<string> Brands { get; set; } = new();
    public List<string> Cities { get; set; } = new();
    public List<string> BanTypes { get; set; } = new();
    public List<int> Years { get; set; } = new();
}

