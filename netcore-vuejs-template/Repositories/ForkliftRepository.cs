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
    public class ForkliftRepository : IForkliftRepository
    {
        public ForkliftRepository()
        {

        }

        public IEnumerable<Forklift> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Forklifts";
            var results = DBContext.GetRecords<Models.Forklift>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<Forklift>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Forklifts";
            var results = await DBContext.GetRecordsAsync<Models.Forklift>(qry.ToString());
            return results;

        }
        public Forklift Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from Forklifts where ForkliftId= '" + id + "'";
            var result = DBContext.GetRecord<Models.Forklift>(qry);
            return result;
        }

        public Forklift GetByForkliftNum(string forkLiftNum)
        {
            if (String.IsNullOrEmpty(forkLiftNum)) return null;
            string qry = $"Select top 1 * from Forklifts where {nameof(Forklift.ForkliftNum)}= '{forkLiftNum}'";
            var result = DBContext.GetRecord<Models.Forklift>(qry);
            return result;
        }

        public Forklift Create(Forklift Forklift)
        {

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(Forklift.ForkliftId).Parameterize(), ParameterValue = Forklift.ForkliftId },
                new ParameterInfo() { ParameterName = nameof(Forklift.ForkliftNum).Parameterize(), ParameterValue = Forklift.ForkliftNum.ToUpper() }
            };

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into Forklifts (");
            qry.AppendLine(nameof(Forklift.ForkliftNum));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(Forklift.ForkliftNum).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            Forklift.ForkliftId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return Forklift;
        }

        public Forklift Update(Forklift ForkliftChanges)
        {
            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(ForkliftChanges.ForkliftId).Parameterize(), ParameterValue = ForkliftChanges.ForkliftId },
                new ParameterInfo() { ParameterName = nameof(ForkliftChanges.ForkliftNum).Parameterize(), ParameterValue = ForkliftChanges.ForkliftNum.ToUpper() },
                new ParameterInfo() { ParameterName = nameof(ForkliftChanges.UpdatedTareWt).Parameterize(), ParameterValue = ForkliftChanges.UpdatedTareWt }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Forklifts set");
            qry.AppendLine($"{nameof(ForkliftChanges.ForkliftNum)}={nameof(ForkliftChanges.ForkliftNum).Parameterize()},");
            qry.AppendLine($"{nameof(ForkliftChanges.UpdatedTareWt)}={nameof(ForkliftChanges.UpdatedTareWt).Parameterize()} ");
            qry.AppendLine($"where {nameof(ForkliftChanges.ForkliftId)} = {nameof(ForkliftChanges.ForkliftId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return ForkliftChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from Forklifts where ForkliftId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }
    }
}