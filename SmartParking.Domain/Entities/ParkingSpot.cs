namespace SmartParking.Domain.Entities;

public class ParkingSpot
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = "Available";
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<SpotStatusLog> StatusLogs { get; set; } = new List<SpotStatusLog>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<UserFavorite> FavoritedBy { get; set; } = new List<UserFavorite>();
}