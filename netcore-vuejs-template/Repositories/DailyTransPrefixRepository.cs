using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;
using System;
using System.Data;

namespace WeighingSystemCore.Repositories
{
    public class DailyTransPrefixRepository : IDailyTransPrefixRepository
    {
        public DailyTransPrefixRepository()
        {

        }

        public DailyTransPrefix Create(DailyTransPrefix DailyTransPrefix)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.DailyTransPrefixId).Parameterize(), ParameterValue = DailyTransPrefix.DailyTransPrefixId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.Prefix).Parameterize(), ParameterValue = DailyTransPrefix.Prefix.ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.EffectiveDate).Parameterize(), ParameterValue = DailyTransPrefix.EffectiveDate });

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into DailyTransPrefixes (");
            qry.AppendLine(nameof(DailyTransPrefix.Prefix) + (char)44);
            qry.AppendLine(nameof(DailyTransPrefix.EffectiveDate));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(DailyTransPrefix.Prefix).Parameterize() + (char)44);
            qry.AppendLine(nameof(DailyTransPrefix.EffectiveDate).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            DailyTransPrefix.DailyTransPrefixId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return DailyTransPrefix;
        }

        public void Delete(long[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from DailyTransPrefixes where DailyTransPrefixId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

        public DailyTransPrefix Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from DailyTransPrefixes where DailyTransPrefixId= '" + id + "'";
            var result = DBContext.GetRecord<Models.DailyTransPrefix>(qry);
            return result;
        }

        public IEnumerable<DailyTransPrefix> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from DailyTransPrefixes order by EffectiveDate desc";
            var results = DBContext.GetRecords<Models.DailyTransPrefix>(qry.ToString());
            return results;
        }

        public DailyTransPrefix Update(DailyTransPrefix dailyTPChanges)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.DailyTransPrefixId).Parameterize(), ParameterValue = dailyTPChanges.DailyTransPrefixId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.Prefix).Parameterize(), ParameterValue = (dailyTPChanges.Prefix ?? string.Empty).ToUpper() });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.EffectiveDate).Parameterize(), ParameterValue = dailyTPChanges.EffectiveDate.Date });

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update DailyTransPrefixes set");
            qry.AppendLine($"{nameof(dailyTPChanges.Prefix)}={nameof(dailyTPChanges.Prefix).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(dailyTPChanges.EffectiveDate)}={nameof(dailyTPChanges.EffectiveDate).Parameterize()}");
            qry.AppendLine($"where {nameof(dailyTPChanges.DailyTransPrefixId)} = {nameof(dailyTPChanges.DailyTransPrefixId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return dailyTPChanges;
        }

        public string GetCurrentPrefix(DateTime dt)
        {
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(DailyTransPrefix.EffectiveDate), ParameterDbType = DbType.DateTime });

            var qry = new StringBuilder();
            qry.AppendLine(" Declare @SelectedPrefix varchar(2); ");
            qry.AppendLine($" set @SelectedPrefix  = (select top 1 prefix from DailyTransPrefixes where {nameof(DailyTransPrefix.EffectiveDate)} = {nameof(DailyTransPrefix.EffectiveDate).Parameterize()}) ");
            qry.AppendLine(" if (@SelectedPrefix is null)  ");
            qry.AppendLine(" begin ");
            qry.AppendLine(" 	set @SelectedPrefix =  (select top 1 prefix from DailyTransPrefixes order by EffectiveDate desc) ");
            qry.AppendLine(" end ");
            qry.AppendLine(" select @SelectedPrefix as Prefix ");

            var result = DBContext.GetRecord<Models.DailyTransPrefix>(qry.ToString(), parameters);


            return result == null ? null : result.Prefix;
        }

    }
}