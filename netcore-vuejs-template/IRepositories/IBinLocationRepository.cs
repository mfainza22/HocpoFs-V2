using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IBinLocationRepository
{
    IEnumerable<BinLocation> List(string qry = "");
    BinLocation Get(Int64 id);
    BinLocation Get(string desc);

    BinLocation Create(BinLocation binLoc);
    BinLocation Update(BinLocation binLocChanges);
    void Delete(string[] ids);
}