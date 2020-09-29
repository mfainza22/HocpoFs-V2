using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;

public interface ITransferLimitAdjRepository
{
    //IEnumerable<TransferLimitAdj> List(DateTime EffectiveDate, long RawMaterialId, long ShiftId);

    IEnumerable<TransferLimitAdj> List(long id);

    TransferLimitAdj Get(Int64 id);
    TransferLimitAdj Create(TransferLimitAdj model);
    TransferLimitAdj Update(TransferLimitAdj modelChanges);
    void Delete(string[] ids);


}