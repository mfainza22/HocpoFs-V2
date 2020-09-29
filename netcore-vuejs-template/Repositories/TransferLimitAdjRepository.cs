using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System;
using WeighingSystemCore.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeighingSystemCore.Repositories
{
    public class TransferLimitAdjRepository : ITransferLimitAdjRepository
    {

        public TransferLimitAdjRepository()
        {
        }

        public TransferLimitAdj Create(TransferLimitAdj model)
        {

            var parameters = initParameters(model);

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into TransferLimitAdjs (");
            qry.AppendLine(nameof(model.AdjDate) + (char)44);
            qry.AppendLine(nameof(model.TransferLimitId) + (char)44);
            qry.AppendLine(nameof(model.AdjLimit) + (char)44);
            qry.AppendLine(nameof(model.AdjRemarks) + (char)44);
            qry.AppendLine(nameof(model.AdjCreatedById));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(model.AdjDate).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.TransferLimitId).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.AdjLimit).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.AdjRemarks).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.AdjCreatedById).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            model.TransferLimitId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return model;
        }

        public TransferLimitAdj Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from TransferLimitAdjs where TransferLimitAdjId= '" + id + "'";
            var result = DBContext.GetRecord<TransferLimitAdj>(qry);
            return result;
        }

        public IEnumerable<TransferLimitAdj> List(long id)
        {

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.TransferLimitId).Parameterize(), ParameterValue = id, ParameterDbType = System.Data.DbType.Int64 });

            var qry = new StringBuilder();
            qry.AppendLine($"Select TransferLimitAdjId,TransferLimitId,AdjDate,AdjLimit,AdjRemarks, AdjCreatedById, UA.FullName as AdjCreatedBy  from TransferLimitAdjs");
            qry.AppendLine(" LEFT OUTER JOIN UserAccounts as UA on UA.id = TransferLimitAdjs.AdjCreatedById");
            qry.AppendLine($"where {nameof(TransferLimitAdj.TransferLimitId)} = {nameof(TransferLimitAdj.TransferLimitId).Parameterize()}");
            var results = DBContext.GetRecords<TransferLimitAdj>(qry.ToString(), parameters).ToList();

            if (results.Count() > 0)
            {
                results.OrderByDescending(a => a.AdjDate).FirstOrDefault().IsActive = true;
            }

            return results;
        }

        public TransferLimitAdj Update(TransferLimitAdj modelChanges)
        {
            var parameters = initParameters(modelChanges);

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update TransferLimitAdjs set");
            qry.AppendLine($"{nameof(modelChanges.AdjLimit)}={nameof(modelChanges.AdjLimit).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(modelChanges.AdjRemarks)}={nameof(modelChanges.AdjRemarks).Parameterize()}");
            qry.AppendLine($"where {nameof(modelChanges.TransferLimitAdjId)} = {nameof(modelChanges.TransferLimitAdjId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return modelChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from TransferLimitAdjs where TransferLimitAdjId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

        private List<ParameterInfo> initParameters(TransferLimitAdj model)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.TransferLimitAdjId).Parameterize(), ParameterValue = model.TransferLimitAdjId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.TransferLimitId).Parameterize(), ParameterValue = model.TransferLimitId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.AdjDate).Parameterize(), ParameterValue = model.AdjDate, ParameterDbType = System.Data.DbType.Date });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.AdjLimit).Parameterize(), ParameterValue = model.AdjLimit });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.AdjRemarks).Parameterize(), ParameterValue = (model.AdjRemarks??string.Empty).ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimitAdj.AdjCreatedById).Parameterize(), ParameterValue = model.AdjCreatedById });
            return parameters;
        }

    }
}