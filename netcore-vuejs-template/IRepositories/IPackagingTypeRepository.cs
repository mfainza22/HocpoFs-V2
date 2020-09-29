using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IPackagingTypeRepository
{
    IEnumerable<PackagingType> List(string qry = "");
    PackagingType Get(Int64 id);
    PackagingType Get(string desc);
    PackagingType Create(PackagingType packagingType);
    PackagingType Update(PackagingType packagingTypeChanges);
    void Delete(string[] ids);
}