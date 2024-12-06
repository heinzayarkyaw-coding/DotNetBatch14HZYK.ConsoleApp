using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;
[Table("tbl_User")]
public class UserModel
{
    [Key]
    public string? user_id { get; set; } = Guid.NewGuid().ToString();
    public string? user_name { get; set; }
    public string? mobile_number { get; set; }
    public decimal? balance { get; set; } = 10000;
    public string? password { get; set; }
}

public class UserResponseModel
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}