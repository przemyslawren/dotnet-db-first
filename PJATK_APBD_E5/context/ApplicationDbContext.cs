using PJATK_APBD_E5.models;

namespace PJATK_APBD_E5.context;

using Microsoft.EntityFrameworkCore;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Client> Client { get; set; }
    public virtual DbSet<Client_Trip> Client_Trip { get; set; }
    public virtual DbSet<Country> Country { get; set; }
    public virtual DbSet<Country_Trip> Country_Trip { get; set; }
    public virtual DbSet<Trip> Trip { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer("Server=db-mssql16.pjwstk.edu.pl;Database=2019SBD;Trusted_Connection=True;TrustServerCertificate=True;")
            .LogTo(Console.WriteLine, LogLevel.Information);
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(120);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Telephone).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Pesel).IsRequired().HasMaxLength(120);
            entity.Property(e => e.IdClient).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Client_Trip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip });
            entity.Property(e => e.RegisteredAt).IsRequired();
            entity.HasOne(e => e.Client)
                .WithMany(c => c.Client_Trips)
                .HasForeignKey(e => e.IdClient);
            entity.HasOne(e => e.Trip)
                .WithMany(t => t.Client_Trips)
                .HasForeignKey(e => e.IdTrip);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
        });

        modelBuilder.Entity<Country_Trip>(entity =>
        {
            entity.HasKey(e => new { e.IdCountry, e.IdTrip });
            entity.HasOne(e => e.Country)
                .WithMany(c => c.Country_Trips)
                .HasForeignKey(e => e.IdCountry);
            entity.HasOne(e => e.Trip)
                .WithMany(t => t.Country_Trips)
                .HasForeignKey(e => e.IdTrip);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(220);
            entity.Property(e => e.DateFrom).IsRequired();
            entity.Property(e => e.DateTo).IsRequired();
            entity.Property(e => e.MaxPeople).IsRequired();
        });
    }
}
