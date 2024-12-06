using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniPosSystemHZYK.RestApi.POS;

[Table("tbl_Product")]
    public class ProductModel
    {
    internal string product_id;

    [Key]
        public string? Product_id { get; set; } = Guid.NewGuid().ToString();
        public string? Product_name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

    }

    public class ProductResponseModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

