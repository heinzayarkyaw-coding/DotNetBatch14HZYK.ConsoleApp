using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14HZYK.ConsoleApp
{
    internal class AppSetting
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder { get; } = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-MMRAUNPQ",
            InitialCatalog = "Textdb",
            UserID = "sa",
            Password = "Hlay1082001",
            TrustServerCertificate = true
        };
    }
}
