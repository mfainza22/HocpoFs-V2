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
    public class PackagingTypeRepository : IPackagingTypeRepository
    {
        public PackagingTypeRepository()
        {

        }

        public PackagingType Create(PackagingType packagingType)
        {
            packagingType.PackagingTypeDesc = packagingType.PackagingTypeDesc.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(packagingType.PackagingTypeId).Parameterize(), ParameterValue = packagingType.PackagingTypeId },
                new ParameterInfo() { ParameterName = nameof(packagingType.PackagingTypeDesc).Parameterize(), ParameterValue = packagingType.PackagingTypeDesc },
                new ParameterInfo() { ParameterName = nameof(packagingType.EmptyPackageWt).Parameterize(), ParameterValue = packagingType.EmptyPackageWt }
            };

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into PackagingTypes (");
            qry.AppendLine(nameof(packagingType.PackagingTypeDesc) + (Char)44);
            qry.AppendLine(nameof(packagingType.EmptyPackageWt));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(packagingType.PackagingTypeDesc).Parameterize() + (Char)44);
            qry.AppendLine(nameof(packagingType.EmptyPackageWt).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            packagingType.PackagingTypeId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return packagingType;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine($"Delete from PackagingTypes where {nameof(PackagingType.PackagingTypeId)} in  ({strIds})");
            DBContext.ExecuteQuery(qry.ToString());
        }

        public PackagingType Get(long id)
        {
            if (id == 0) return null;
            string qry = $"Select top 1 * from PackagingTypes where {nameof(PackagingType.PackagingTypeId)}= '" + id + "'";
            var result = DBContext.GetRecord<Models.PackagingType>(qry);
            return result;
        }
        public PackagingType Get(string desc)
        {
            if (String.IsNullOrEmpty(desc)) return null;
            string qry = $"Select top 1 * from PackagingTypes where {nameof(PackagingType.PackagingTypeDesc)}= '" + desc + "'";
            var result = DBContext.GetRecord<Models.PackagingType>(qry);
            return result;
        }
        public IEnumerable<PackagingType> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from PackagingTypes";
            var results = DBContext.GetRecords<Models.PackagingType>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<PackagingType>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from PackagingTypes";
            var results = await DBContext.GetRecordsAsync<Models.PackagingType>(qry.ToString());
            return results;
        }

        public PackagingType Update(PackagingType PackagingTypeChanges)
        {
            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(PackagingTypeChanges.PackagingTypeId).Parameterize(), ParameterValue = PackagingTypeChanges.PackagingTypeId },
                new ParameterInfo() { ParameterName = nameof(PackagingTypeChanges.PackagingTypeDesc).Parameterize(), ParameterValue = PackagingTypeChanges.PackagingTypeDesc },
                new ParameterInfo() { ParameterName = nameof(PackagingTypeChanges.EmptyPackageWt).Parameterize(), ParameterValue = PackagingTypeChanges.EmptyPackageWt }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update PackagingTypes set");
            qry.AppendLine($"{nameof(PackagingTypeChanges.PackagingTypeDesc)}={nameof(PackagingTypeChanges.PackagingTypeDesc).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(PackagingTypeChanges.EmptyPackageWt)}={nameof(PackagingTypeChanges.EmptyPackageWt).Parameterize()}");
            qry.AppendLine($"where {nameof(PackagingTypeChanges.PackagingTypeId)} = {nameof(PackagingTypeChanges.PackagingTypeId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return PackagingTypeChanges;
        }


    }
}