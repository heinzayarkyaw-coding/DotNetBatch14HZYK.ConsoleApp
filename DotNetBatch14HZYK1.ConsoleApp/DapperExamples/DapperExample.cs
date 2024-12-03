using Dapper;
using DotNetBatch14HZYK.ConsoleApp.Dtos;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14HZYK.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly string _connectionString = AppSetting.SqlConnectionStringBuilder.ConnectionString;

        public void Read()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            List<BlogDto> lst = connection.Query<BlogDto>("select * from tbl_blog").ToList();

            foreach (BlogDto blog in lst)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
            }
        }

        public void Edit(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var item = connection.Query<BlogDto>($"select * from tbl_blog where BlogId ='{id}'").FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
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

            using IDbConnection connection =new SqlConnection(_connectionString);
            var result = connection.Execute(query);

            string message = result > 0 ? "Create Successful" : "Create Failed";
            Console.WriteLine(message);


        }

        public void Delete(string id)
        {
            string query = $"DELETE FROM [dbo].[tbl_blog] WHERE BlogId ='{id}'";

            using IDbConnection connection = new SqlConnection(_connectionString);
            var result = connection.Execute(query, new { Id = id });

            string message = result > 0 ? "Delete" : "No Delete";
            Console.WriteLine(message); 
        }

        public void Update(string id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[tbl_blog] SET BlogTitle = @title, BlogAuthor = @author, BlogContent = @content WHERE BlogId = @id";

            using IDbConnection connection = new SqlConnection(_connectionString);
            var result = connection.Execute(query, new {id = id, title = title, author =author, content = content});

            string message = result > 0 ? "Update" : "No  Update";
            Console.WriteLine(message); 
        }
    }
}
