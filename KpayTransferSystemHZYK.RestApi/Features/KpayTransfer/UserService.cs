using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace KpayTransferSystemHZYK.RestApi.Features.KpayTransfer;

public class UserService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<UserModel> GetUsers()
    {
        List<UserModel> lst = _db.Users!.ToList();
        return lst;
    }

    public UserModel GetUser(string id)
    {
        var item = _db.Users!.AsNoTracking().FirstOrDefault(x => x.user_id == id);
        return item!;
    }

    public UserResponseModel CreateUser(UserModel requestUser)
    {
        UserResponseModel model = new UserResponseModel();
        var name = _db.Users!.AsNoTracking().FirstOrDefault(x => x.user_name == requestUser.user_name);
        if (name is not null)
        {
            model.IsSuccess = false;
            model.Message = "UserName already exist!";
            return model;
        }

        var phone = _db.Users!.AsNoTracking().FirstOrDefault(x => x.mobile_number == requestUser.mobile_number);
        if (phone is not null)
        {
            model.IsSuccess = false;
            model.Message = "Phone number already exist!";
            return model;
        }

        _db.Users!.Add(requestUser);
        var result = _db.SaveChanges();

        string message = result > 0 ? "Create Successful." : "Create Failed";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public UserResponseModel UpdateUser(UserModel requestUser)
    {
        var user = _db.Users!.AsNoTracking().FirstOrDefault(x => x.user_id == requestUser.user_id);
        if (user is null)
        {
            return new UserResponseModel()
            {
                IsSuccess = false,
                Message = "No user found!"
            };
        }

        if (!string.IsNullOrEmpty(requestUser.user_name))
        {
            user.user_name = requestUser.user_name;
        }
        if (!string.IsNullOrEmpty(requestUser.mobile_number))
        {
            user.mobile_number = requestUser.mobile_number;
        }
        if (!string.IsNullOrEmpty(requestUser.password))
        {
            user.password = requestUser.password;
        }

        _db.Entry(user).State = EntityState.Modified;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Update Successful." : "Update Failed.";
        UserResponseModel model = new UserResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public UserResponseModel UpsertUser(UserModel requestUser)
    {
        UserResponseModel model = new UserResponseModel();

        var user = _db.Users!.AsNoTracking().FirstOrDefault(x => x.user_id == requestUser.user_id);
        if (user is not null)
        {
            if (!string.IsNullOrEmpty(requestUser.user_name))
            {
                user.user_name = requestUser.user_name;
            }
            if (!string.IsNullOrEmpty(requestUser.mobile_number))
            {
                user.mobile_number = requestUser.mobile_number;
            }
            if (!string.IsNullOrEmpty(requestUser.password))
            {
                user.password = requestUser.password;
            }

            _db.Entry(user).State = EntityState.Modified;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Update Successful." : "Update Failed.";
            model.IsSuccess = result > 0;
            model.Message = message;
        }
        else if (user is null)
        {
            model = CreateUser(requestUser);
        }

        return model;
    }

    public UserResponseModel DeleteUser(string id)
    {
        UserResponseModel model = new UserResponseModel();

        var user = _db.Users!.AsNoTracking().FirstOrDefault(x => x.user_id == id);
        if (user is null)
        {
            model.IsSuccess = false;
            model.Message = "No user found!";
        }

#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        _db.Entry(user).State = EntityState.Deleted;
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        var result = _db.SaveChanges();

        string message = result > 0 ? "Delete Successful." : "Delete Failed.";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }
public UserResponseModel Deposit(UserDepositModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();

        var user = _db.Users.AsNoTracking().FirstOrDefault(x => x.mobile_number == requestModel.mobile_number);
        if (user is null)
        {
            model.IsSuccess = false;
            model.Message = "Incorrect Mobile Number!";
            return model;
        }

        user.balance += requestModel.balance;

        _db.Entry(user).State = EntityState.Modified;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Deposit Success." : "Deposit Fail!";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }

    public UserResponseModel Withdraw(UserWithdrawModel requestModel)
    {
        UserResponseModel model = new UserResponseModel();

        var user = _db.Users.AsNoTracking().FirstOrDefault(x => x.mobile_number == requestModel.mobile_number);
        if (user is null)
        {
            model.IsSuccess = false;
            model.Message = "Incorrect Mobile Number";
            return model;
        }

        user.balance -= requestModel.balance;

        _db.Entry(user).State = EntityState.Modified;
        var result = _db.SaveChanges();

        string message = result > 0 ? "Withdraw Success." : "Withdraw Fail!";
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }
}
