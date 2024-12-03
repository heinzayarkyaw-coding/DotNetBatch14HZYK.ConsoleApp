using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14HZYK.RestApi.features.Blog
{
    public class BlogService
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-MMRAUNPQ",
            InitialCatalog = "Textdb",
            UserID = "sa",
            Password = "Hlay1082001",
            TrustServerCertificate = true

        };
        public List<BlogModel> GetBlogs()
        {
            SqlConnection Connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select * from tbl_blog", Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Connection.Close();

            List<BlogModel> lst = new List<BlogModel>();

            foreach (DataRow row in dt.Rows)
            {

                BlogModel item = new BlogModel();
                item.BlogId = row["BlogId"].ToString()!;
                item.BlogTitle = row["BlogTitle"].ToString()!;
                item.BlogAuthor = row["BlogAuthor"].ToString()!;
                item.BlogContent = row["BlogContent"].ToString()!;

                lst.Add(item);
            }
            return lst;

        }

        public BlogModel GetBlog(string id)
        {
            SqlConnection Connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            Connection.Open();

            SqlCommand cmd = new SqlCommand("select * from tbl_blog where BlogId =@BlogId;", Connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Connection.Close();


            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];

            BlogModel item = new BlogModel();
            item.BlogId = row["BlogId"].ToString()!;
            item.BlogTitle = row["BlogTitle"].ToString()!;
            item.BlogAuthor = row["BlogAuthor"].ToString()!;
            item.BlogContent = row["BlogContent"].ToString()!;

            return item;



        }

        public BlogResponseModel CreateBlog(BlogModel requestModel)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";



            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Success." : "Saving Fail.";
            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;
            return model;

        }

        public BlogResponseModel UpdateBlog(BlogModel requestModel)
        {
            var item = GetBlog(requestModel.BlogId);
            if (item is null)
            {
                return new BlogResponseModel
                {
                    IsSuccess = false,
                    Message = "No Data Found"
                };
            }

            if (string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                requestModel.BlogTitle = item.BlogTitle;
            }

            if (string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                requestModel.BlogAuthor = item.BlogAuthor;
            }

            if (string.IsNullOrEmpty(requestModel.BlogContent))
            {
                requestModel.BlogContent = item.BlogContent;
            }

            string query = @"UPDATE [dbo].[tbl_blog]
                     SET BlogTitle = @BlogTitle,
                         BlogAuthor = @BlogAuthor,
                         BlogContent = @BlogContent
                     WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Saving Success." : "Saving Fail.";
            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;
            return model;

        }


        public BlogResponseModel UpsertBlog(BlogModel requestModel)
        {
            BlogResponseModel model = new BlogResponseModel();

            var item = GetBlog(requestModel.BlogId!);
            if (item is not null)
            {
                string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

                #region Update Database

                SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
                cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
                int result = cmd.ExecuteNonQuery();

                connection.Close();

                #endregion

                string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                model.IsSuccess = result > 0;
                model.Message = message;
            }
            else if (item is null)
            {
                model = CreateBlog(requestModel);
            }

            return model;
        }

        public BlogResponseModel DeleteBlog(string id)
        {
            SqlConnection con = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("Delete from [dbo].[Tbl_Blog] where BlogId = @BlogId", con);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            con.Close();

            string message = result > 0 ? "Delete Success." : "Delete Fail!";
            BlogResponseModel model = new BlogResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }
    }
}