using System.Collections.Generic;
using WeighingSystemCore.Models;
using DataAccessLayer;
using System.Text;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Repositories
{
    public class WeighingAreaRepository : IWeighingAreaRepository
    {
        public WeighingAreaRepository()
        {

        }

        public IEnumerable<WeighingArea> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from WeighingAreas";
            var results = DBContext.GetRecords<Models.WeighingArea>(qry.ToString());
            return results;
        }

        public WeighingArea Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from WeighingAreas where WeighingAreaId= '" + id + "'";
            var result = DBContext.GetRecord<Models.WeighingArea>(qry);
            return result;
        }

        public WeighingArea GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return null;
            string qry = $"Select top 1 * from WeighingAreas where {nameof(WeighingArea.AreaCode)}= '{code}'";
            var result = DBContext.GetRecord<Models.WeighingArea>(qry);
            return result;
        }

        public WeighingArea GetByDesc(string desc)
        {
            if (string.IsNullOrEmpty(desc)) return null;
            string qry = $"Select top 1 * from WeighingAreas where {nameof(WeighingArea.AreaDesc)}= '{desc}'";
            var result = DBContext.GetRecord<Models.WeighingArea>(qry);
            return result;
        }

        public WeighingArea Create(WeighingArea weighingArea)
        {
            weighingArea.AreaCode = weighingArea.AreaCode.ToUpperCase();
            weighingArea.AreaDesc = weighingArea.AreaDesc.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(weighingArea.WeighingAreaId).Parameterize(), ParameterValue = weighingArea.WeighingAreaId },
                new ParameterInfo() { ParameterName = nameof(weighingArea.AreaCode).Parameterize(), ParameterValue = weighingArea.AreaCode },
                new ParameterInfo() { ParameterName = nameof(weighingArea.AreaDesc).Parameterize(), ParameterValue = weighingArea.AreaDesc }
            };

            var qry = new StringBuilder();

            qry.AppendLine("insert into WeighingAreas (");
            qry.AppendLine(nameof(weighingArea.AreaCode) + (char)44);
            qry.AppendLine(nameof(weighingArea.AreaDesc));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(weighingArea.AreaCode).Parameterize() + (char)44);
            qry.AppendLine(nameof(weighingArea.AreaDesc).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            weighingArea.WeighingAreaId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return weighingArea;
        }

        public WeighingArea Update(WeighingArea weighingAreaChanges)
        {
            weighingAreaChanges.AreaCode = weighingAreaChanges.AreaCode.ToUpperCase();
            weighingAreaChanges.AreaDesc = weighingAreaChanges.AreaDesc.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(WeighingArea.WeighingAreaId).Parameterize(), ParameterValue = weighingAreaChanges.WeighingAreaId },
                new ParameterInfo() { ParameterName = nameof(WeighingArea.AreaCode).Parameterize(), ParameterValue = weighingAreaChanges.AreaCode },
                new ParameterInfo() { ParameterName = nameof(WeighingArea.AreaDesc).Parameterize(), ParameterValue = weighingAreaChanges.AreaDesc }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update WeighingAreas set");
            qry.AppendLine($"{nameof(weighingAreaChanges.AreaCode)}={nameof(weighingAreaChanges.AreaCode).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(weighingAreaChanges.AreaDesc)}={nameof(weighingAreaChanges.AreaDesc).Parameterize()}");
            qry.AppendLine($"where {nameof(weighingAreaChanges.WeighingAreaId)} = {nameof(weighingAreaChanges.WeighingAreaId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return weighingAreaChanges;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from WeighingAreas where WeighingAreaId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }

        public WeighingArea GetDefault()
        {
            string qry = $"Select top 1 * from WeighingAreas";

            var result = DBContext.GetRecord<Models.WeighingArea>(qry);

            return result;
        }



    }
}