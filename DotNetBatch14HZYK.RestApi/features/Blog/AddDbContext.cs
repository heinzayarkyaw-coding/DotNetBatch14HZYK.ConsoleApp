using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14HZYK.RestApi.features.Blog
{
    public class AppDbContext : DbContext
    {

        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public AppDbContext()
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "LAPTOP-MMRAUNPQ",
                InitialCatalog = "Textdb",
                UserID = "sa",
                Password = "Hlay1082001",
                TrustServerCertificate = true
            };

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_sqlConnectionStringBuilder.ConnectionString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}
