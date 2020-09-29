using DataAccessLayer;
using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IRefNumRepository
{
    RefNum Get(Int64 id);
    RefNum Update(RefNum refNumChanges);

    TransactionQuery GetQuery(Int64 id = 1);

    TransactionQuery GetUpdateQuery(RefNum refNumChanges);
}