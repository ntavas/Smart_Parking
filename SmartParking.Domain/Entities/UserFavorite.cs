namespace SmartParking.Domain.Entities;

public class UserFavorite
{
    public int UserId { get; set; }
    public int SpotId { get; set; }

    // Navigation
    public User User { get; set; } = null!;
    public ParkingSpot ParkingSpot { get; set; } = null!;
}