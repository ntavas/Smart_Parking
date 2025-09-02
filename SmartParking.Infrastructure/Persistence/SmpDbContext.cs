using Microsoft.EntityFrameworkCore;
using SmartParking.Domain.Entities;

namespace SmartParking.Infrastructure.Persistence;

public class SmpDbContext : DbContext
{
    public SmpDbContext(DbContextOptions<SmpDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ParkingSpot> ParkingSpots => Set<ParkingSpot>();
    public DbSet<SpotStatusLog> SpotStatusLogs => Set<SpotStatusLog>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<UserFavorite> UserFavorites => Set<UserFavorite>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.HasKey(u => u.Id).HasName("pk_users");
            e.Property(u => u.Id).HasColumnName("id").UseIdentityByDefaultColumn();
            e.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(100);
            e.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
            e.Property(u => u.FullName).HasColumnName("full_name").HasMaxLength(100);
            e.Property(u => u.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            e.HasIndex(u => u.Email).IsUnique().HasDatabaseName("uq_users_email");
        });

        // ParkingSpot
        modelBuilder.Entity<ParkingSpot>(e =>
        {
            e.ToTable("parking_spots");
            e.HasKey(p => p.Id).HasName("pk_parking_spots");
            e.Property(p => p.Id).HasColumnName("id").UseIdentityByDefaultColumn();
            e.Property(p => p.Latitude).HasColumnName("latitude");
            e.Property(p => p.Longitude).HasColumnName("longitude");
            e.Property(p => p.Location).HasColumnName("location").IsRequired().HasMaxLength(100);
            e.Property(p => p.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("Available");
            e.Property(p => p.LastUpdated).HasColumnName("last_updated").HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            e.HasIndex(p => p.Status).HasDatabaseName("idx_parking_spots_status");
            e.HasIndex(p => p.Location).HasDatabaseName("idx_parking_spots_location");
        });

        // SpotStatusLog
        modelBuilder.Entity<SpotStatusLog>(e =>
        {
            e.ToTable("spot_status_log");
            e.HasKey(s => s.Id).HasName("pk_spot_status_log");
            e.Property(s => s.Id).HasColumnName("id").UseIdentityByDefaultColumn();
            e.Property(s => s.SpotId).HasColumnName("spot_id");
            e.Property(s => s.Status).HasColumnName("status").HasMaxLength(20);
            e.Property(s => s.Timestamp).HasColumnName("timestamp").HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            e.HasOne<ParkingSpot>()
             .WithMany(p => p.StatusLogs)
             .HasForeignKey(s => s.SpotId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Reservation
        modelBuilder.Entity<Reservation>(e =>
        {
            e.ToTable("reservations");
            e.HasKey(r => r.Id).HasName("pk_reservations");
            e.Property(r => r.Id).HasColumnName("id").UseIdentityByDefaultColumn();
            e.Property(r => r.UserId).HasColumnName("user_id");     // ✅ Explicit
            e.Property(r => r.SpotId).HasColumnName("spot_id");
            e.Property(r => r.StartTime).HasColumnName("start_time");
            e.Property(r => r.EndTime).HasColumnName("end_time");

            e.HasOne(r => r.User)                            // ✅ Use lambda
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)                   // ✅ Explicit FK
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(r => r.ParkingSpot)
                .WithMany(p => p.Reservations)
                .HasForeignKey(r => r.SpotId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // UserFavorite (Composite Key)
        modelBuilder.Entity<UserFavorite>(e =>
        {
            e.ToTable("user_favorites");
            e.HasKey(uf => new { uf.UserId, uf.SpotId });

            e.Property(uf => uf.UserId).HasColumnName("user_id");
            e.Property(uf => uf.SpotId).HasColumnName("spot_id");

            e.HasOne(uf => uf.User)
             .WithMany(u => u.Favorites)
             .HasForeignKey(uf => uf.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(uf => uf.ParkingSpot)
             .WithMany(p => p.FavoritedBy)
             .HasForeignKey(uf => uf.SpotId)
             .OnDelete(DeleteBehavior.Cascade);
        });
    }
}