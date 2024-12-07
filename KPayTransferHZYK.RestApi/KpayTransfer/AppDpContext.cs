using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KPayTransferHZYK.RestApi.KpayTransfer;

public class AppDpContext
{

    private readonly SqlConnectionStringBuilder _conntectionBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "LAPTOP-MMRAUNPQ",
        InitialCatalog = "KpayTransferSystem",
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
    public DbSet<DepositModel>? TransfersHistory { get; set; }
    //public DbSet<UserModel>? Users { get; set; }
}
