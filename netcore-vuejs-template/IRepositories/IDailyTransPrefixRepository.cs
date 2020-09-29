using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IDailyTransPrefixRepository
{
    IEnumerable<DailyTransPrefix> List(string qry = "");
    DailyTransPrefix Get(Int64 id);
    DailyTransPrefix Create(DailyTransPrefix dailyTransPrefix);
    DailyTransPrefix Update(DailyTransPrefix dailTransPrefixChanges);
    void Delete(long[] ids);

    string GetCurrentPrefix(DateTime dt);

}