using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface ILocationRepository
{
    IEnumerable<Location> List(string qry = "");
    Location Get(Int64 id);
    Location Get(string name);
    Location Create(Location location);
    Location Update(Location locationChanges);
    void Delete(string[] ids);
    Location GetDefault(long currentId = 0);


}