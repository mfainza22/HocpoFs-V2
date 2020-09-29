using System;
using System.Collections.Generic;
using WeighingSystemCore.Models;

public interface IClientMachineRepository
{
    IEnumerable<ClientMachine> List(string qry = "");
    ClientMachine Get(Int64 id);
    ClientMachine Create(ClientMachine forkLift);
    ClientMachine Update(ClientMachine forkLiftChanges);
    void Delete(long[] ids);

    ClientMachine GetByIP(string ip);

    ClientMachine CheckClientMachine(string ip);

}