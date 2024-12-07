using Microsoft.EntityFrameworkCore;

namespace MiniPosSystemHZYK.RestApi.POS;

public class Product_CategoryService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<CategoryModel> GetCategories()
    {
        List<CategoryModel> lst = _db.Category.AsNoTracking().ToList();
        return lst;
    }

    public CategoryModel GetCategory(string id)
    {
        var item = _db.Category.AsNoTracking().FirstOrDefault(x => x.category_id == id);
        return item!;
    }

    public CategoryResponseModel CreateCategory(CategoryModel requestModel)
    {
        var item = _db.Category.AsNoTracking().FirstOrDefault(x => x.category_name == requestModel.category_name);
        if (item is not null)
        {
            return new CategoryResponseModel
            {
                IsSuccess = false,
                Message = "Category already exist!"
            };
        }

        _db.Add(requestModel);
        var result = _db.SaveChanges();

        string message = result > 0 ? "Create Success." : "Create Fail!";
        CategoryResponseModel model = new CategoryResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public CategoryResponseModel UpdateCategory(CategoryModel requestModel)
    {
        var item = _db.Category!.AsNoTracking().FirstOrDefault(x => x.category_id == requestModel.category_id);
        if (item is null)
        {
            return new CategoryResponseModel()
            {
                IsSuccess = false,
                Message = "No item found!"
            };
        }

        if (!string.IsNullOrEmpty(requestModel.category_name))
        {
            item.category_name = requestModel.category_name;
        }
        if (!string.IsNullOrEmpty(requestModel.category_code))
        {
            item.category_code = requestModel.category_code;
        }

        _db.Entry(item).State = EntityState.Modified;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Update Success." : "Update Fail!";
        CategoryResponseModel model = new CategoryResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public CategoryResponseModel UpsertCategory(CategoryModel requestModel)
    {
        CategoryResponseModel model = new CategoryResponseModel();

        var item = _db.Category!.AsNoTracking().FirstOrDefault(x => x.category_id == requestModel.category_id);
        if (item is not null)
        {
            if (!string.IsNullOrEmpty(requestModel.category_name))
            {
                item.category_name = requestModel.category_name;
            }
            if (!string.IsNullOrEmpty(requestModel.category_code))
            {
                item.category_code = requestModel.category_code;
            }

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Update Success." : "Update Fail!";
            model.IsSuccess = result > 0;
            model.Message = message;
        }
        else if (item is null)
        {
            model = CreateCategory(requestModel);
        }

        return model;
    }

    public CategoryResponseModel DeleteCategory(string id)
    {
        CategoryResponseModel model = new CategoryResponseModel();

        var item = _db.Category!.AsNoTracking().FirstOrDefault(x => x.category_id == id);
        if (item is null)
        {
            model.IsSuccess = false;
            model.Message = "No item found!";
            return model;
        }

        _db.Entry(item).State = EntityState.Deleted;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Delete Success." : "Delete Fail!";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }
}
