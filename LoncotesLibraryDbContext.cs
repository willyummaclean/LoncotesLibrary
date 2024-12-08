using Microsoft.EntityFrameworkCore;
using Library.Models;

public class LoncotesLibaryDbContext : DbContext
{

    public DbSet<Genre> Genres { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    public LoncotesLibaryDbContext(DbContextOptions<LoncotesLibaryDbContext> context) : base(context)
    {

    }
}