using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IForkliftRepository
{
    IEnumerable<Forklift> List(string qry = "");
    Forklift Get(Int64 id);
    Forklift GetByForkliftNum(string forkliftNum);
    Forklift Create(Forklift forkLift);
    Forklift Update(Forklift forkLiftChanges);
    void Delete(string[] ids);
}