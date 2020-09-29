using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IRawMaterialRepository
{
    IEnumerable<RawMaterial> List(string qry = "");
    RawMaterial Get(Int64 id);
    RawMaterial GetByCode(string code);
    RawMaterial GetByDesc(string desc);

    RawMaterial Create(RawMaterial rawMaterial);
    RawMaterial Update(RawMaterial rawMaterialChanges);
    void Delete(string[] ids);
}