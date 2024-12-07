using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniPosSystemHZYK.RestApi.POS;
[Table("tbl_product_category")]

public class CategoryModel
{
    [Key]
    public string? category_id { get; set; } = Guid.NewGuid().ToString();
    public string? category_name { get; set; }
    public string? category_code { get; set; }
}

public class CategoryResponseModel
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}