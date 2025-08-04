// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using PdfReportApi.Models;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CustomerTransaction> CustomerTransactions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerTransaction>()
            .HasNoKey();

        base.OnModelCreating(modelBuilder);
    }

}
