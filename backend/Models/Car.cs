namespace TurboAz.Api.Models;

public class Car
{
    public string? Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string BanType { get; set; } = string.Empty;
    public int Odometer { get; set; }
    public string OdometerUnit { get; set; } = "km";
    public decimal Price { get; set; }
    public string Currency { get; set; } = "AZN";
    public string Year { get; set; } = string.Empty;
    public decimal Engine { get; set; }
    public bool? Credit { get; set; }
    public bool? Barter { get; set; }
    public List<string> Images { get; set; } = new();
    public string City { get; set; } = string.Empty;
    public string Dates { get; set; } = string.Empty;
}

