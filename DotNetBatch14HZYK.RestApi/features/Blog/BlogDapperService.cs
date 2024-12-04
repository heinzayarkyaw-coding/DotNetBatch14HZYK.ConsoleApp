using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14HZYK.RestApi.features.Blog;

public class BlogDapperService : IBlogService
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

    public BlogDapperService()
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


    public List<BlogModel> GetBlogs()
    {
        string query = "select * from tbl_blog with (nolock) ";
        using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);


        var lst = db.Query<BlogModel>(query).ToList();
        return lst;

    }

    public BlogModel GetBlog(string id)
    {


        string query = "select * from tbl_blog with (nolock) ";
        using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);


        var item = db.QueryFirstOrDefault<BlogModel>(query);
        return item!;


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


        using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, requestModel);


        string message = result > 0 ? "Saving Success." : "Saving Fail.";
        BlogResponseModel model = new BlogResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;
        return model;

    }

    public BlogResponseModel UpdateBlog(BlogModel requestModel)
    {
        var item = GetBlog(requestModel.BlogId!);
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

        using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, requestModel);

        string message = result > 0 ? "Update Success." : "Update Fail.";
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


            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, requestModel);

            #endregion

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

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
        string query = "Delete from [dbo].[Tbl_Blog] where BlogId = @BlogId";

        using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        var result = db.Execute(query, new BlogModel
        {
            BlogId = id
        });

        string message = result > 0 ? "Delete Success." : "Delete Fail!";
        BlogResponseModel model = new BlogResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

}
