﻿using MiniPosSystemHZYK.RestApi.POS;
using static MiniPosHZYK.RestApi.Features.SaleModel;

namespace MiniPosHZYK.RestApi.Features;

public class SaleService
{

    private readonly AppDbContext _db = new AppDbContext();

    public List<SaleModel> GetHistories()
    {
        List<SaleModel> lst = _db.Sale!.ToList();
        return lst;
    }

    public SaleModel GetHistory(string id)
    {
        var item = _db.Sale!.FirstOrDefault(x => x.sale_id == id);
        return item!;
    }

    public SaleResponseModel CreateHistory(SaleModel requestModel)
    {
        _db.Add(requestModel);
        var result = _db.SaveChanges();

        string message = result > 0 ? "Create Success." : "Create Fail!";
        SaleResponseModel model = new SaleResponseModel();
        model.IsSuccess = result > 0;
        model.Message = message;

        return model;
    }
}

