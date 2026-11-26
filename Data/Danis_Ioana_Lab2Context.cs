using Microsoft.EntityFrameworkCore;

public class Danis_Ioana_Lab2Context(DbContextOptions<Danis_Ioana_Lab2Context> options) : DbContext(options)
{
    public DbSet<Danis_Ioana_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Danis_Ioana_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Danis_Ioana_Lab2.Models.Authors> Authors { get; set; } = default!;
    public DbSet<Danis_Ioana_Lab2.Models.Category> Category { get; set; } = default!;
    public DbSet<Danis_Ioana_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
}
