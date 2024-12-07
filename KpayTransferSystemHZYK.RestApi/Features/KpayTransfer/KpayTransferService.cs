using Microsoft.EntityFrameworkCore;

namespace KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;

public class KpayTransferService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<KpayTransferModel> GetRecords()
    {
        List<KpayTransferModel> lst = _db.TransfersHistory!.ToList();
        return lst;
    }

    public KpayTransferModel GetRecord(string id)
    {
        var item = _db.TransfersHistory!.AsNoTracking().FirstOrDefault(x => x.transaction_id == id);
        return item!;
    }

    public TransferResponseModel CreateTransfer(UserModel user, string fromMobile, string toMobile, string password, decimal amount, string notes)
    {
        TransferResponseModel model = new TransferResponseModel();

        var sender = _db.Users!.FirstOrDefault(x => x.mobile_number == fromMobile);
        if (sender is null)
        {
            model.IsSuccess = false;
            model.Message = "Create account first with this mobile number!";
            return model;
        }

        if (sender.password != password)
        {
            model.IsSuccess = false;
            model.Message = "Incorrect Password!";
            return model;
        }
        if (sender.balance <= 10000 || (sender.balance - 10000) <= amount)
        {
            model.IsSuccess = false;
            model.Message = "Not enough balance!";
            return model;
        }

        var receiver = _db.Users!.FirstOrDefault(x => x.mobile_number == toMobile);
        if (receiver is null)
        {
            model.IsSuccess = false;
            model.Message = "There's no account with this mobile number";
            return model;
        }

        sender.balance -= amount;
        receiver.balance += amount;

        var history = new KpayTransferModel
        {
            transaction_id = Guid.NewGuid().ToString(),
            from_mobile_number = fromMobile,
            to_mobile_number = toMobile,
            amount = amount,
            transaction_date = DateTime.Now,
            notes = notes,
        };

        _db.TransfersHistory!.Add(history);
        int result = _db.SaveChanges();

        string message = result > 0 ? "Successfully Transfer." : "Transfer Fail!";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }
}