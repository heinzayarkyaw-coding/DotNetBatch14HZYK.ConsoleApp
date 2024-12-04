
namespace DotNetBatch14HZYK.RestApi.features.Blog
{
    public interface IBlogService
    {
        BlogResponseModel CreateBlog(BlogModel requestModel);
        BlogResponseModel DeleteBlog(string id);
        BlogModel GetBlog(string id);
        List<BlogModel> GetBlogs();
        BlogResponseModel UpdateBlog(BlogModel requestModel);
        BlogResponseModel UpsertBlog(BlogModel requestModel);
    }
}