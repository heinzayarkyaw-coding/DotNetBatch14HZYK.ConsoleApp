using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniPosHZYK.RestApi.Features;

[Table("tbl_sale_details")]
public class SaleDetailsModel
{
    [Key]
    public string? SaleDetailsId { get; set; } = Guid.NewGuid().ToString();
    public string? SaleId { get; set; }
    public string? ProductId { get; set; }
    public int? SaleTotalQty { get; set; }
    public decimal? SaleTotalAmoutn { get; set; }
}

public class SaleDetailResponseModel
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}
