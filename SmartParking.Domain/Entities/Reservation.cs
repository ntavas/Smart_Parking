namespace SmartParking.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SpotId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }

    // Navigation
    public User User { get; set; } = null!;
    public ParkingSpot ParkingSpot { get; set; } = null!;
}