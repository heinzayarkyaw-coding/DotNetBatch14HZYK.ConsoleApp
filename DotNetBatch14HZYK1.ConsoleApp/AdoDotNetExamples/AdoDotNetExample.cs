using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14HZYK.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-MMRAUNPQ",
            InitialCatalog = "Textdb",
            UserID = "sa",
            Password = "Hlay1082001",
            TrustServerCertificate = true

        };

        public void Read()
        {
            SqlConnection Connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select * from tbl_blog", Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Connection.Close();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["BlogId"]);
                Console.WriteLine(row["BlogTitle"]);
                Console.WriteLine(row["BlogAuthor"]);
                Console.WriteLine(row["BlogContent"]);
            }

        }

        public void Edit(string id)
        { 
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand($"select * from tbl_blog where BlogId ='{id}' ", connection);
            SqlDataAdapter adapter = new SqlDataAdapter( cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection .Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow row = dt.Rows[0];

            Console.WriteLine(row["BlogId"]);
            Console.WriteLine(row["BlogTitle"]);
            Console.WriteLine(row["BlogAuthor"]);
            Console.WriteLine(row["BlogContent"]);


        }

        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('{title}'
           ,'{author}'
           ,'{content}')";



            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Success." : "Saving Fail.";
            Console.WriteLine(message);
        }

        public void Delete(string id)
        {
            string query = $"DELETE FROM [dbo].[tbl_blog] WHERE BlogId = '{id}'";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString); 
            connection.Open();

            SqlCommand sql = new SqlCommand(query, connection);
            int result = sql.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? $"Delete Success" : "Delete Fail";
            Console.WriteLine(message);


        }

        public void Update(string id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[tbl_blog]
                     SET BlogTitle = @Title,
                         BlogAuthor = @Author,
                         BlogContent = @Content
                     WHERE BlogId = @Id";

            using (SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Content", content);


                    int result = cmd.ExecuteNonQuery();

                    string message = result > 0 ? $"Update Successful for BlogId {id}" : $"Update Failed for BlogId {id}.";
                    Console.WriteLine(message);
                }
            }
        }

      

    }
}
