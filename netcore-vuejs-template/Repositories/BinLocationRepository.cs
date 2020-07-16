using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WeighingSystemCore.Repositories
{
    public class BinLocationRepository : IBinLocationRepository
    {
        public BinLocationRepository()
        {

        }

        public BinLocation Create(BinLocation BinLocation)
        {

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(BinLocation.BinLocationId).Parameterize(), ParameterValue = BinLocation.BinLocationId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(BinLocation.BinLocDesc).Parameterize(), ParameterValue = BinLocation.BinLocDesc.ToUpper() });

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into BinLocations (");
            qry.AppendLine(nameof(BinLocation.BinLocDesc));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(BinLocation.BinLocDesc).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            BinLocation.BinLocationId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return BinLocation;
        }

        public BinLocation Get([FromRoute] long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from BinLocations where BinLocationId= '" + id + "'";
            var result = DBContext.GetRecord<Models.BinLocation>(qry);
            return result;
        }

        public IEnumerable<BinLocation> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from BinLocations";
            var results = DBContext.GetRecords<Models.BinLocation>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<BinLocation>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from BinLocations";
            var results = await DBContext.GetRecordsAsync<Models.BinLocation>(qry.ToString());
            return results;
        }

        public BinLocation Update(BinLocation BinLocationChanges)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(BinLocationChanges.BinLocationId).Parameterize(), ParameterValue = BinLocationChanges.BinLocationId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(BinLocationChanges.BinLocDesc).Parameterize(), ParameterValue = BinLocationChanges.BinLocDesc.ToUpper() });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update BinLocations set");
            qry.AppendLine($"{nameof(BinLocationChanges.BinLocDesc)}={nameof(BinLocationChanges.BinLocDesc).Parameterize()}");
            qry.AppendLine($"where {nameof(BinLocationChanges.BinLocationId)} = {nameof(BinLocationChanges.BinLocationId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return BinLocationChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from BinLocations where BinLocationId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

    }
}