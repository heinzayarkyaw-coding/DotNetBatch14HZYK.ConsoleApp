using KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KpayTransferSystemHZYK.RestApi.Features;

[Route("api/[controller]")]
[ApiController]
public class WithdrawController : ControllerBase
{
    private readonly UserService _userService;

    public WithdrawController()
    {
        _userService = new UserService();
    }
    [HttpPost]

    public IActionResult Withdraw([FromBody] UserWithdrawModel requestModel)
    {
        var model = _userService.Withdraw(requestModel);
        if (!model.IsSuccess)
        {
            return BadRequest(model);
        }
        return Ok(model);
    }
}
