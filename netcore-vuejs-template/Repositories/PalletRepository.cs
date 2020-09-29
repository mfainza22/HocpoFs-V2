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
    public class PalletRepository : IPalletRepository
    {
        public PalletRepository()
        {

        }

        public Pallet Create(Pallet pallet)
        {
            pallet.PalletNum = pallet.PalletNum.ToUpperCase();
            pallet.PalletType = pallet.PalletType.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(pallet.PalletId).Parameterize(), ParameterValue = pallet.PalletId },
                new ParameterInfo() { ParameterName = nameof(pallet.PalletNum).Parameterize(), ParameterValue = pallet.PalletNum },
                new ParameterInfo() { ParameterName = nameof(pallet.PalletType).Parameterize(), ParameterValue = pallet.PalletType },
                new ParameterInfo() { ParameterName = nameof(pallet.PalletTypeId).Parameterize(), ParameterValue = pallet.PalletTypeId },
                new ParameterInfo() { ParameterName = nameof(pallet.UpdatedWt).Parameterize(), ParameterValue = pallet.UpdatedWt },
                new ParameterInfo() { ParameterName = nameof(pallet.EstimatedWt).Parameterize(), ParameterValue = pallet.EstimatedWt }
            };

            StringBuilder qry = new StringBuilder();

            qry.AppendLine("insert into Pallets (");
            qry.AppendLine(nameof(pallet.PalletNum) + (char)44);
            qry.AppendLine(nameof(pallet.PalletType) + (char)44);
            qry.AppendLine(nameof(pallet.PalletTypeId) + (char)44);
            qry.AppendLine(nameof(pallet.UpdatedWt) + (char)44);
            qry.AppendLine(nameof(pallet.EstimatedWt));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(pallet.PalletNum).Parameterize() + (char)44);
            qry.AppendLine(nameof(pallet.PalletType).Parameterize() + (char)44);
            qry.AppendLine(nameof(pallet.PalletTypeId).Parameterize() + (char)44);
            qry.AppendLine(nameof(pallet.UpdatedWt).Parameterize() + (char)44);
            qry.AppendLine(nameof(pallet.EstimatedWt).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            pallet.PalletId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return pallet;
        }

        public void Delete(string[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from Pallets where PalletId in  ({0})", strIds));
            DBContext.ExecuteQuery(qry.ToString());
        }

        public Pallet Get(long id)
        {
            if (id == 0) return null;
            string qry = "Select top 1 * from Pallets where PalletId= '" + id + "'";
            var result = DBContext.GetRecord<Models.Pallet>(qry);
            return result;
        }

        public Pallet Get(string palletNum)
        {
            if (String.IsNullOrEmpty(palletNum)) return null;
            string qry = "Select top 1 * from Pallets where PalletNum= '" + palletNum.ToUpperCase().Trim() + "'";
            var result = DBContext.GetRecord<Models.Pallet>(qry);
            return result;
        }

        public decimal GetUpdatedWtByName(string palletNum)
        {
            if (String.IsNullOrEmpty(palletNum)) return 0;
            string qry = "Select UpdatedWt  from Pallets where PalletNum= '" + palletNum.ToUpperCase().Trim() + "'";
            var pallet = DBContext.GetRecord<Models.Pallet>(qry);

            return pallet == null ? 0 : pallet.UpdatedWt;
        }

        public IEnumerable<Pallet> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Pallets";
            var results = DBContext.GetRecords<Models.Pallet>(qry.ToString());
            return results;
        }

        public async Task<IEnumerable<Pallet>> ListAsync(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from Pallets";
            var results = await DBContext.GetRecordsAsync<Models.Pallet>(qry.ToString());
            return results;
        }

        public Pallet Update(Pallet palletChanges)
        {
            palletChanges.PalletNum = palletChanges.PalletNum.ToUpperCase();
            palletChanges.PalletType = palletChanges.PalletType.ToUpperCase();

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletId).Parameterize(), ParameterValue = palletChanges.PalletId },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletNum).Parameterize(), ParameterValue = (palletChanges.PalletNum ?? String.Empty) },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletType).Parameterize(), ParameterValue = (palletChanges.PalletType ?? String.Empty) },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletTypeId).Parameterize(), ParameterValue = palletChanges.PalletTypeId },
                new ParameterInfo() { ParameterName = nameof(Pallet.UpdatedWt).Parameterize(), ParameterValue = palletChanges.UpdatedWt },
                new ParameterInfo() { ParameterName = nameof(Pallet.EstimatedWt).Parameterize(), ParameterValue = palletChanges.EstimatedWt }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Pallets set");
            qry.AppendLine($"{nameof(palletChanges.PalletNum)}={nameof(palletChanges.PalletNum).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(palletChanges.PalletType)}={nameof(palletChanges.PalletType).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(palletChanges.PalletTypeId)}={nameof(palletChanges.PalletTypeId).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(palletChanges.UpdatedWt)}={nameof(palletChanges.UpdatedWt).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(palletChanges.EstimatedWt)}={nameof(palletChanges.EstimatedWt).Parameterize()}");
            qry.AppendLine($"where {nameof(palletChanges.PalletId)} = {nameof(palletChanges.PalletId).Parameterize()}");
            DBContext.ExecuteQuery(qry.ToString(), parameters);
            return palletChanges;
        }

        public TransactionQuery GetUpdateWtQuery(Pallet palletChanges)
        {
            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletId).Parameterize(), ParameterValue = palletChanges.PalletId },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletNum).Parameterize(), ParameterValue = palletChanges.PalletNum },
                new ParameterInfo() { ParameterName = nameof(Pallet.UpdatedWt).Parameterize(), ParameterValue = palletChanges.UpdatedWt }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("Update Pallets set");
            qry.AppendLine($"{nameof(palletChanges.UpdatedWt)}={nameof(Pallet.UpdatedWt).Parameterize()}");
            qry.AppendLine($"where {nameof(palletChanges.PalletNum)} = {nameof(Pallet.PalletNum).Parameterize()}");

            return new TransactionQuery() { Parameters = parameters, Query = qry };
        }

        public TransactionQuery CheckAndInsert(Pallet pallet)
        {
            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletId).Parameterize(), ParameterValue = pallet.PalletId },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletNum).Parameterize(), ParameterValue = (pallet.PalletNum ?? String.Empty) },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletType).Parameterize(), ParameterValue = (pallet.PalletType ?? String.Empty) },
                new ParameterInfo() { ParameterName = nameof(Pallet.PalletTypeId).Parameterize(), ParameterValue = pallet.PalletTypeId },
                new ParameterInfo() { ParameterName = nameof(Pallet.UpdatedWt).Parameterize(), ParameterValue = pallet.UpdatedWt },
                new ParameterInfo() { ParameterName = nameof(Pallet.EstimatedWt).Parameterize(), ParameterValue = pallet.EstimatedWt }
            };

            StringBuilder qry = new StringBuilder();
            qry.AppendLine($" IF NOT EXISTS (select * from pallets where PalletNum = @PalletNum)");
            qry.AppendLine(" BEGIN ");
            qry.AppendLine("insert into Pallets (");
            qry.AppendLine(nameof(Pallet.PalletNum) + (char)44);
            qry.AppendLine(nameof(Pallet.PalletType) + (char)44);
            qry.AppendLine(nameof(Pallet.PalletTypeId) + (char)44);
            qry.AppendLine(nameof(Pallet.UpdatedWt) + (char)44);
            qry.AppendLine(nameof(Pallet.EstimatedWt));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(Pallet.PalletNum).Parameterize() + (char)44);
            qry.AppendLine(nameof(Pallet.PalletType).Parameterize() + (char)44);
            qry.AppendLine(nameof(Pallet.PalletTypeId).Parameterize() + (char)44);
            qry.AppendLine(nameof(Pallet.UpdatedWt).Parameterize() + (char)44);
            qry.AppendLine(nameof(Pallet.EstimatedWt).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("END");
            qry.AppendLine("ELSE");
            qry.AppendLine("BEGIN");
            qry.AppendLine("Update Pallets set");
            qry.AppendLine($"{nameof(pallet.UpdatedWt)}={nameof(pallet.UpdatedWt).Parameterize()}");
            qry.AppendLine($"where {nameof(pallet.PalletNum)} = {nameof(pallet.PalletNum).Parameterize()}");
            qry.AppendLine(" END ");


            return new TransactionQuery() { Parameters = parameters, Query = qry };

        }
    }
}