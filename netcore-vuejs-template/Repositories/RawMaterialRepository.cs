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
    public class RawMaterialRepository : IRawMaterialRepository
    {
        public RawMaterialRepository()
        {

        }

        public RawMaterial Create(RawMaterial rawMaterial)
        {

            rawMaterial.RawMaterialCode = rawMaterial.RawMaterialCode.ToUpperCase();
            rawMaterial.RawMaterialDesc = rawMaterial.RawMaterialCode.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(rawMaterial.RawMaterialId).Parameterize(), ParameterValue = rawMaterial.RawMaterialId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(rawMaterial.RawMaterialCode).Parameterize(), ParameterValue = rawMaterial.RawMaterialCode });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(rawMaterial.RawMaterialDesc).Parameterize(), ParameterValue = rawMaterial.RawMaterialDesc });

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into RawMaterials (");   
            qry.AppendLine(nameof(rawMaterial.RawMaterialCode) + (char)44);
            qry.AppendLine(nameof(rawMaterial.RawMaterialDesc));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(rawMaterial.RawMaterialCode).Parameterize() + (char)44);
            qry.AppendLine(nameof(rawMaterial.RawMaterialDesc).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            rawMaterial.RawMaterialId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return rawMaterial;
        }

       

        public RawMaterial Get([FromRoute] long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from RawMaterials where RawMaterialId= '" + id + "'";
            var result = DBContext.GetRecord<Models.RawMaterial>(qry);
            return result;
        }

        public RawMaterial GetByCode(string code)
        {
            if (String.IsNullOrEmpty(code)) return null;
            string qry = $"Select top 1 * from RawMaterials where {nameof(RawMaterial.RawMaterialCode)}= '" + code.ToUpperCase().Trim() + "'";
            var result = DBContext.GetRecord<Models.RawMaterial>(qry);
            return result;
        }

        public RawMaterial GetByDesc(string desc)
        {
            if (String.IsNullOrEmpty(desc)) return null;
            string qry = $"Select top 1 * from RawMaterials where {nameof(RawMaterial.RawMaterialDesc)}= '" + desc.ToUpperCase().Trim() + "'";
            var result = DBContext.GetRecord<Models.RawMaterial>(qry);
            return result;
        }

        public IEnumerable<RawMaterial> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from RawMaterials";
            var results = DBContext.GetRecords<Models.RawMaterial>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<RawMaterial>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from RawMaterials";
            var results = await DBContext.GetRecordsAsync<Models.RawMaterial>(qry.ToString());
            return results;
        }

        public RawMaterial Update(RawMaterial rawMaterialChanges)
        {
            rawMaterialChanges.RawMaterialCode = rawMaterialChanges.RawMaterialCode.ToUpperCase();
            rawMaterialChanges.RawMaterialDesc = rawMaterialChanges.RawMaterialCode.ToUpperCase();

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RawMaterial.RawMaterialId).Parameterize(), ParameterValue = rawMaterialChanges.RawMaterialId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RawMaterial.RawMaterialCode).Parameterize(), ParameterValue = rawMaterialChanges.RawMaterialCode });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(RawMaterial.RawMaterialDesc).Parameterize(), ParameterValue = rawMaterialChanges.RawMaterialDesc });


            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update RawMaterials set");
            qry.AppendLine($"{nameof(rawMaterialChanges.RawMaterialCode)}={nameof(rawMaterialChanges.RawMaterialCode).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(rawMaterialChanges.RawMaterialDesc)}={nameof(rawMaterialChanges.RawMaterialDesc).Parameterize()}");
            qry.AppendLine($"where {nameof(rawMaterialChanges.RawMaterialId)} = {nameof(rawMaterialChanges.RawMaterialId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return rawMaterialChanges;
        }
        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from RawMaterials where RawMaterialId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

    }
}