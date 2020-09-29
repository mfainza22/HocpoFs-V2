using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace WeighingSystemCore.Repositories
{
    public class ClientMachineRepository : IClientMachineRepository
    {
        private readonly IWeighingAreaRepository waRepository;

        public ClientMachineRepository(IWeighingAreaRepository _waRepository)
        {
            waRepository = _waRepository;
        }

        public ClientMachine Create(ClientMachine ClientMachine)
        {

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.ClientMachineId).Parameterize(), ParameterValue = ClientMachine.ClientMachineId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.ClientIP).Parameterize(), ParameterValue = ClientMachine.ClientIP.ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.WeighingAreaId).Parameterize(), ParameterValue = ClientMachine.WeighingAreaId });

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into ClientMachines (");
            qry.AppendLine(nameof(ClientMachine.ClientIP) + (char)44);
            qry.AppendLine(nameof(ClientMachine.WeighingAreaId));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(ClientMachine.ClientIP).Parameterize() + (char)44);
            qry.AppendLine(nameof(ClientMachine.WeighingAreaId).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            ClientMachine.ClientMachineId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return ClientMachine;
        }

        public void Delete(long[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from ClientMachines where ClientMachineId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

        public ClientMachine Get([FromRoute] long id)
        {
            if (id == 0) return null;
            string qry = "select * from ClientMachines left outer join WeighingAreas as WA on WA.WeighingAreaId =ClientMachines.WeighingAreaId where ClientMachineId= '" + id + "'";
            var result = DBContext.GetRecord<Models.ClientMachine>(qry);
            return result;
        }

        public IEnumerable<ClientMachine> List(string qry = "")
        {
            if (qry == string.Empty) qry = "select * from ClientMachines left outer join WeighingAreas as WA on WA.WeighingAreaId =ClientMachines.WeighingAreaId ";
            var results = DBContext.GetRecords<Models.ClientMachine>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<ClientMachine>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "select * from ClientMachines left outer join WeighingAreas as WA on WA.WeighingAreaId =ClientMachines.WeighingAreaId";
            var results = await DBContext.GetRecordsAsync<Models.ClientMachine>(qry.ToString());
            return results;
        }

        public ClientMachine Update(ClientMachine clientMachineChanges)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.ClientMachineId).Parameterize(), ParameterValue = clientMachineChanges.ClientMachineId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.ClientIP).Parameterize(), ParameterValue = clientMachineChanges.ClientIP.ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(ClientMachine.WeighingAreaId).Parameterize(), ParameterValue = clientMachineChanges.WeighingAreaId });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update ClientMachines set");
            qry.AppendLine($"{nameof(clientMachineChanges.ClientIP)}={nameof(clientMachineChanges.ClientIP).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(clientMachineChanges.WeighingAreaId)}={nameof(clientMachineChanges.WeighingAreaId).Parameterize()}");
            qry.AppendLine($"where {nameof(clientMachineChanges.ClientMachineId)} = {nameof(clientMachineChanges.ClientMachineId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return clientMachineChanges;
        }

        public ClientMachine GetByIP(string ip)
        {
            if (string.IsNullOrEmpty(ip)) return null;
            string qry = "select top 1 * from ClientMachines left outer join WeighingAreas as WA on WA.WeighingAreaId =ClientMachines.WeighingAreaId where ClientIP= '" + ip + "'";
            var result = DBContext.GetRecord<Models.ClientMachine>(qry);
            return result;
        }

        public ClientMachine CheckClientMachine(string ip)
        {
            var cm = GetByIP(ip);

            if (cm == null)
            {
                cm = new ClientMachine();
                cm.ClientIP = ip;
                var defWA = waRepository.GetDefault();
                cm.WeighingAreaId = defWA.WeighingAreaId;
                Create(cm);
            }

            return cm;
        }

    }
}