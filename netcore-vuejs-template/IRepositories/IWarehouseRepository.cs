using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IWarehouseRepository
{
    IEnumerable<Warehouse> List(string qry = "");
    Warehouse Get(Int64 id);
    Warehouse GetByWarehouseCode(string code);
    Warehouse GetByWarehouseName(string name);
    Warehouse Create(Warehouse warehouse);
    Warehouse Update(Warehouse warehouseChanges);
    void Delete(string[] ids);
    Warehouse GetDefault(long currentId = 0);


}