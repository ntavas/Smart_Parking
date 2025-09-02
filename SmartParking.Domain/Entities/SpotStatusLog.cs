namespace SmartParking.Domain.Entities;

public class SpotStatusLog
{
    public int Id { get; set; }
    public int SpotId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation
    public ParkingSpot ParkingSpot { get; set; } = null!;
}