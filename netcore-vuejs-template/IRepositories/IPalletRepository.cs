using DataAccessLayer;
using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IPalletRepository
{
    IEnumerable<Pallet> List(string qry = "");
    Pallet Get(Int64 id);
    Pallet Get(string palletNum);
    decimal GetUpdatedWtByName(string palletNum);

    Pallet Create(Pallet pallet);
    Pallet Update(Pallet palletChanges);
    void Delete(string[] ids);

    TransactionQuery GetUpdateWtQuery(Pallet palletChanges);

    TransactionQuery CheckAndInsert(Pallet pallet);
}