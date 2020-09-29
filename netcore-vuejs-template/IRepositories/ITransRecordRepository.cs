using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Enums;

public interface ITransRecordRepository
{
    IList<TransRecordViewModel> List(string query);
    TransRecordViewModel Get(Int64 id,Enums.TransactionStatus transactionStatus);
    TransRecordViewModel WeighIn(TransRecordViewModel transRecord);
    TransRecordViewModel WeighOut(TransRecordViewModel transRecordChanges);
    TransRecordViewModel Update(TransRecordViewModel transRecordChanges, Enums.TransactionStatus transactionStatus);
    void Delete(string[] ids, Enums.TransactionStatus transactionStatus);
}