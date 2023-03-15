using Microsoft.EntityFrameworkCore;
using Northwind.CosmosDb.Models;

namespace Northwind.CosmosDb.Data;

public class NorthwindContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos(
            accountEndpoint: "https://localhost:8081",
            accountKey: "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "nortwind-db");

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
               .ToContainer("Employees") // ToContainer
               .HasPartitionKey(e => e.Id); // Partition Key

        // configuring Customers
        modelBuilder.Entity<Customer>()
            .ToContainer("Customers") // ToContainer
            .HasPartitionKey(c => c.Id); // Partition Key

        modelBuilder.Entity<Customer>().OwnsMany(p => p.Orders);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
}
