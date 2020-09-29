using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;

public interface ITransferLimitRepository
{
    public TransferLimitViewModel Get(long id);

    public TransferLimitViewModel Get(TransferLimitViewModel model);

    public TransferLimitViewModel2 GetStatus(TransferLimitViewModel model);

    public TransferLimitData GetTransferLimitData(TransferLimitViewModel model);

    IEnumerable<TransferLimitViewModel> List(string qry = "");
    IEnumerable<TransferLimitViewModel> ListByEffectiveDate(DateTime? dt);
    IEnumerable<TransferLimitViewModel2> ListByEffectiveDate2(DateTime? dt);

    TransferLimitViewModel Create(TransferLimitViewModel model);

    TransferLimitViewModel Update(TransferLimitViewModel modelChanges);

    void Delete(long[] ids);    

    TransferLimitViewModel CheckLimit(TransferLimitViewModel model);
    void AutoGenTransferLimit(DateTime dt, string userId);

}