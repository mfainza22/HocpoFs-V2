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

        public BinLocation Create(BinLocation binLocation)
        {
            binLocation.BinLocDesc = binLocation.BinLocDesc.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(binLocation.BinLocationId).Parameterize(), ParameterValue = binLocation.BinLocationId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(binLocation.BinLocDesc).Parameterize(), ParameterValue = binLocation.BinLocDesc });

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into BinLocations (");
            qry.AppendLine(nameof(binLocation.BinLocDesc));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(binLocation.BinLocDesc).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            binLocation.BinLocationId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return binLocation;
        }

        public BinLocation Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from BinLocations where BinLocationId= '" + id + "'";
            var result = DBContext.GetRecord<Models.BinLocation>(qry);
            return result;
        }

        public BinLocation Get(string desc)
        {
            if (String.IsNullOrEmpty(desc)) return null;
            string qry = "Select top 1 * from BinLocations where BinLocDesc= '" + desc + "'";
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

        public BinLocation Update(BinLocation binLocationChanges)
        {
            binLocationChanges.BinLocDesc = binLocationChanges.BinLocDesc.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(binLocationChanges.BinLocationId).Parameterize(), ParameterValue = binLocationChanges.BinLocationId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(binLocationChanges.BinLocDesc).Parameterize(), ParameterValue = binLocationChanges.BinLocDesc });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update BinLocations set");
            qry.AppendLine($"{nameof(binLocationChanges.BinLocDesc)}={nameof(binLocationChanges.BinLocDesc).Parameterize()}");
            qry.AppendLine($"where {nameof(binLocationChanges.BinLocationId)} = {nameof(binLocationChanges.BinLocationId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return binLocationChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from BinLocations where BinLocationId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }

    }
}