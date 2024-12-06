using Microsoft.EntityFrameworkCore;

namespace MiniPosSystemHZYK.RestApi.POS;

 public class ProductService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> lst = _db.Product.AsNoTracking().ToList();
            return lst;
        }

        public ProductModel GetProduct(string id)
        {
            var item = _db.Product.AsNoTracking().FirstOrDefault(x => x.Product_id == id);
            return item!;
        }
        public ProductResponseModel CreateProduct(ProductModel requestModel)
    {
        ProductResponseModel model = new ProductResponseModel();
        var name = _db.Product!.AsNoTracking().FirstOrDefault(x => x.Product_name == requestModel.Product_name);
        if (name is not null)
        {
            model.IsSuccess = false;
            model.Message = "ProductName already exist!";
            return model;
        }

      
        _db.Product!.Add(requestModel);
        var result = _db.SaveChanges();

        string message = result > 0 ? "Create Successful." : "Create Failed";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public ProductResponseModel UpdateProduct(ProductModel requestModel)
    {
        var product = _db.Product!.AsNoTracking().FirstOrDefault(x => x.Product_id == requestModel.Product_id);
        if (product is null)
        {
            return new ProductResponseModel()
            {
                IsSuccess = false,
                Message = "No product found!"
            };
        }

        if (!string.IsNullOrEmpty(requestModel.Product_name))
        {
            product.Product_name = requestModel.Product_name;
        }
        if (!string.IsNullOrEmpty(requestModel.Price.ToString()))
        {
            product.Price = requestModel.Price;
        }
        if (!string.IsNullOrEmpty(requestModel.Quantity.ToString()))
        {
            product.Quantity = requestModel.Quantity;
        }

        _db.Entry(product).State = EntityState.Modified;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Update Successful." : "Update Failed.";
        ProductResponseModel model = new ProductResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public ProductResponseModel UpsertProduct(ProductModel requestModel)
    {
        ProductResponseModel model = new ProductResponseModel();

        var product = _db.Product!.AsNoTracking().FirstOrDefault(x => x.Product_id == requestModel.Product_id);
        if (product is not null)
        {
            if (!string.IsNullOrEmpty(requestModel.Product_name))
            {
                product.Product_name = requestModel.Product_name;
            }
            if (!string.IsNullOrEmpty(requestModel.Price.ToString()))
            {
                product.Price = requestModel.Price;
            }
            if (!string.IsNullOrEmpty(requestModel.Quantity.ToString()))
            {
                product.Quantity = requestModel.Quantity;
            }

            _db.Entry(product).State = EntityState.Modified;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            model.IsSuccess = result > 0;
            model.Message = message;
        }
        else if (product is null)
        {
            model = CreateProduct(requestModel);
        }

        return model;
    }

    public ProductResponseModel DeleteProduct(string id)
    {
        ProductResponseModel model = new ProductResponseModel();

        var product = _db.Product!.AsNoTracking().FirstOrDefault(x => x.Product_id == id);
        if (product is null)
        {
            model.IsSuccess = false;
            model.Message = "No product found!";
        }

#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        _db.Entry(product).State = EntityState.Deleted;
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        var result = _db.SaveChanges();

        string message = result > 0 ? "Delete Successful." : "Delete Failed.";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

  
}
