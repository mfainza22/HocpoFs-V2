using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IWeighingAreaRepository
{
    IEnumerable<WeighingArea> List(string qry = "");
    WeighingArea Get(Int64 id);
    WeighingArea GetByCode(string code);
    WeighingArea GetByDesc(string name);

    WeighingArea Create(WeighingArea warehouse);
    WeighingArea Update(WeighingArea warehouseChanges);
    void Delete(string[] ids);

    WeighingArea GetDefault();


}