using SmartParking.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();
}