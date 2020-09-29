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
    public class RefNumRepository : IRefNumRepository
    {
        public RefNumRepository()
        {

        }

        public RefNum Get(long id = 1)
        {
            if (id == 0) return null;
            string qry = $"Select top 1 * from RefNums where {nameof(RefNum.RefNumId)}= '{id}'";
            var result = DBContext.GetRecord<Models.RefNum>(qry);
            return result;
        }

        public IEnumerable<RefNum> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from RefNums";
            var results = DBContext.GetRecords<Models.RefNum>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<RefNum>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from RefNums";
            var results = await DBContext.GetRecordsAsync<Models.RefNum>(qry.ToString());
            return results;
        }

        public RefNum Update(RefNum RefNumChanges)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RefNum.RefNumId).Parameterize(), ParameterValue = RefNumChanges.RefNumId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RefNum.ReceiptSeriesNum).Parameterize(), ParameterValue = RefNumChanges.ReceiptSeriesNum.ToUpper() });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update RefNums set");
            qry.AppendLine($"{nameof(RefNumChanges.ReceiptSeriesNum)}={nameof(RefNumChanges.ReceiptSeriesNum).Parameterize()}");
            qry.AppendLine($"where {nameof(RefNumChanges.RefNumId)} = {nameof(RefNumChanges.RefNumId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return RefNumChanges;
        }

        public TransactionQuery GetQuery(long id = 1)
        {
            if (id == 0) return null;
            var qry = new StringBuilder();
            qry.AppendLine($"Select ReceiptSeriesNum from RefNums where {nameof(RefNum.RefNumId)}= '{id}'");

            return new TransactionQuery() { Query = qry };
        }

        public TransactionQuery GetUpdateQuery(RefNum refNumChanges)
        {

            refNumChanges.RefNumId = refNumChanges.RefNumId == 0 ? 1 : refNumChanges.RefNumId;
            var newSeriesNum = string.Format("{0:0000000}", Convert.ToInt64(refNumChanges.ReceiptSeriesNum) + 1);
            refNumChanges.ReceiptSeriesNum = newSeriesNum;

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RefNum.RefNumId).Parameterize(), ParameterValue = refNumChanges.RefNumId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RefNum.ReceiptSeriesNum).Parameterize(), ParameterValue = refNumChanges.ReceiptSeriesNum.ToUpper() });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update RefNums set");
            qry.AppendLine($"{nameof(refNumChanges.ReceiptSeriesNum)}={nameof(refNumChanges.ReceiptSeriesNum).Parameterize()}");
            qry.AppendLine($"where {nameof(refNumChanges.RefNumId)} = {nameof(refNumChanges.RefNumId).Parameterize()}");

            return new TransactionQuery() { Query = qry, Parameters = parameters };
        }
    }
}