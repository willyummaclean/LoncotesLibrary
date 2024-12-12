using Microsoft.EntityFrameworkCore;
using Library.Models;

public class LoncotesLibraryDbContext : DbContext
{

    public DbSet<Genre> Genres { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    public LoncotesLibraryDbContext(DbContextOptions<LoncotesLibraryDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed data with campsite types
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
        new Genre {Id = 1, Name = "Horror"},
        new Genre {Id = 2, Name = "Non-Fiction"},
        new Genre {Id = 3, Name = "Music"},
        new Genre {Id = 4, Name = "Silly Putty"}
        });

        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
        new MaterialType {Id = 1, Name = "Book", DaysCheckedOut = 6},
        new MaterialType {Id = 2, Name = "DVD", DaysCheckedOut = 10},
        new MaterialType {Id = 3, Name = "Micofiche", DaysCheckedOut = 3},
        new MaterialType {Id = 4, Name = "Putty", DaysCheckedOut = 21}
        });

        modelBuilder.Entity<Material>().HasData(new Material[]
        {
        new Material {Id = 1, MaterialName = "Dracula", MaterialTypeId = 1, GenreId = 1},
        new Material {Id = 2, MaterialName = "E.T", MaterialTypeId = 2, GenreId = 2},
        new Material {Id = 3, MaterialName = "Silly Putty", MaterialTypeId = 4, GenreId = 4},
        new Material {Id = 4, MaterialName = "Paranoid", MaterialTypeId = 3, GenreId = 3, OutOfCirculationSince = new DateTime(2024, 12, 12)}
        });

        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
        new Patron {Id = 1, FirstName = "Horror", LastName = "Story", Address = "Cave in the Woods", Email = "patron1@email.com", IsActive = true },
        new Patron {Id = 2, FirstName = "Gloria", LastName = "Hood", Address = "Cabin in the Woods", Email = "patron2@email.com", IsActive = true },
        new Patron {Id = 3, FirstName = "Steve", LastName = "Tub", Address = "Pond in the Woods", Email = "patron3@email.com", IsActive = false }
        });

        modelBuilder.Entity<Checkout>().HasData(new Checkout[]
        {
        new Checkout {Id = 1, MaterialId = 1, PatronId = 1, CheckedOutSince = new DateTime(2024, 12, 07)},
        new Checkout {Id = 2, MaterialId = 2, PatronId = 2, CheckedOutSince = new DateTime(2024, 12, 06)},
        new Checkout {Id = 3, MaterialId = 3, PatronId = 3, CheckedOutSince = new DateTime(2024, 12, 05)}
        });

    }
}