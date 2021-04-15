using Microsoft.EntityFrameworkCore;
using static System.Environment;
using static System.IO.Path;

namespace Exercise02
{
  public class Northwind : DbContext
  {
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnConfiguring(
      DbContextOptionsBuilder optionsBuilder)
    {
      string path = Combine(CurrentDirectory, "Northwind.db");
        
      optionsBuilder.UseSqlite($"Filename={path}");
    }
  }
}