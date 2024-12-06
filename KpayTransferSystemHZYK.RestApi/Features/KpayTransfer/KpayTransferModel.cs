using System.ComponentModel.DataAnnotations;

namespace KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;

public class KpayTransferModel
{
    [Key]
    public string? TransactionId { get; set; } = Guid.NewGuid().ToString();
    public string? FromMobileNo { get; set; }
    public string? ToMobileNo { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? Date { get; set; }
    public string? Notes { get; set; }
}

public class TransferResponseModel
{
    public bool? IsSuccess { get; set; }
    public string? Message { get; set; }
}
