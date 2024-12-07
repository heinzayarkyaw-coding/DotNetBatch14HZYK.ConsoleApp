using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniPosHZYK.RestApi.Features;


[Table("tbl_Sale")]
public class SaleModel
{
    [Key]
    [Column("sale_id")]
    public string? sale_id { get; set; } = Guid.NewGuid().ToString();
    public int? quantity { get; set; }
    public decimal? amount { get; set; }
    public DateTime date { get; set; } = DateTime.Now;
}

public class SaleResponseModel
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
