using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniPosHZYK.RestApi.Features;

namespace MiniPosSystemHZYK.RestApi.POS;

public class AppDbContext : DbContext
{
    private readonly SqlConnectionStringBuilder _conntectionBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "LAPTOP-MMRAUNPQ",
        InitialCatalog = "MiniPOSdb",
        UserID = "sa",
        Password = "Hlay1082001",
        TrustServerCertificate = true
    };

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_conntectionBuilder.ConnectionString);
        }
    }
    public DbSet<ProductModel>? Product { get; set; }
    public DbSet<SaleModel>? Sale { get; set; }
    public DbSet<CategoryModel>? Category { get; set; }

    public DbSet<SaleDetailsModel>? SaleDetails { get; set; }
}
