using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;


[Table("tbl_transaction")]
public class KpayTransferModel
{
    [Key]
    public string? transaction_id { get; set; } = Guid.NewGuid().ToString();
    public string? from_mobile_number { get; set; }
    public string? to_mobile_number { get; set; }
    public decimal? amount { get; set; }
    public DateTime? transaction_date { get; set; }
    public string? notes { get; set; }
}

public class TransferRequestModel
{
    public string? from_mobile_number { get; set; }
    public string? to_mobile_number { get; set; }
    public decimal? amount { get; set; }
    public string? password { get; set; }
    public string? notes { get; set; }
}

public class TransferResponseModel
{
    public bool? IsSuccess { get; set; }
    public string? Message { get; set; }
}