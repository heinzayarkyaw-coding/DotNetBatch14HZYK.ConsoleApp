using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<TransactionModel>? TransactionHistory { get; set; }
}
