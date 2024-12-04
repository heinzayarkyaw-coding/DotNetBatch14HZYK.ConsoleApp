using DotNetBatch14HZYK.RestApi.features.BlogDapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14HZYK.RestApi.features.Blog;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;

    public BlogController()
    {
        _blogService = new BlogDapperService();
    }

    [HttpGet]

    public IActionResult GetBlogs()
    {
        var model = _blogService.GetBlogs();
        return Ok(model);
    }


    [HttpGet("{id}")]

    public IActionResult GetBlog(string id)
    {
        var model = _blogService.GetBlog(id);
        if (model == null) 
            return NotFound("No Data Found");

        return Ok(model);
    }


    [HttpPost]

    public IActionResult CreateBlog([FromBody] BlogModel requestModel)
    {
        var model = _blogService.CreateBlog(requestModel);
        if (!model.IsSuccess)
        {
            return BadRequest(model);
        }
        return Ok(model);
    }


    [HttpPut("{id}")]
    public IActionResult UpsertBlog(string id, BlogModel requestModel)
    {
        requestModel.BlogId = id;

        var model = _blogService.UpsertBlog(requestModel);
        if (!model.IsSuccess)
        {
            return BadRequest(model);
        }
        return Ok();
    }



    [HttpPatch("{id}")]

    public IActionResult PatchBlog(string id, BlogModel requestModel)
    {
        requestModel.BlogId = id;

        var model = _blogService.UpdateBlog(requestModel);
        if (!model.IsSuccess)
        {
            return BadRequest(model);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(string id)
    {
        var model = _blogService.DeleteBlog(id);
        if (model is null)
        {
            return BadRequest(model);
        }
        return Ok();
    }
}
