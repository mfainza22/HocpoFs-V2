using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IShiftRepository
{
    IEnumerable<Shift> List(string qry = "");
    Shift Get(Int64 id); 
    Shift GetByCode(string code);
    Shift GetByDesc(string desc);
    Shift Create(Shift group);
    Shift Update(Shift groupChanges);
    void Delete(string[] ids);
    Shift GetCurrentShift(DateTime dt);
}