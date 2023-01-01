using Microsoft.EntityFrameworkCore;

namespace Example.BookStore.Catalog;

public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {

    }
    public DbSet<Book> Books => Set<Book>();

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.LogTo(Console.WriteLine);
    //     optionsBuilder.UseNpgsql("Host=db:5432;Database=Catalog;Username=root;Password=root");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Catalog");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
    }
}
