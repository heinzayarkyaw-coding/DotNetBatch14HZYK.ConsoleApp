using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiniPosSystemHZYK.RestApi.POS;
[Table("tbl_transaction")]
public class TransactionModel
{
    [Key]
    [Column("transaction_id")]
    public string? transaction_id { get; set; } = Guid.NewGuid().ToString();
    public string? product_id { get; set; }
    public int? quantity { get; set; }
    public decimal? total_amount { get; set; }
    public DateTime transaction_date { get; set; } = DateTime.Now;
    public string? payment_method { get; set; }
}

public class TransactionRequestModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}