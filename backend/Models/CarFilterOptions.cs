namespace TurboAz.Api.Models;

public class CarFilterOptions
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public string? City { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public string? Currency { get; set; }
    public string? BanType { get; set; }
    public int? YearMin { get; set; }
    public int? YearMax { get; set; }
    public bool? Credit { get; set; }
    public bool? Barter { get; set; }
    public string? Condition { get; set; } // "all", "new", "used"
}

