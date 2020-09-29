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
using WeighingSystemCoreHelpers.Enums;

namespace WeighingSystemCore.Repositories
{
    public class TransferLimitRepository : ITransferLimitRepository
    {
        private readonly IRawMaterialRepository _rawMatRepo;
        private readonly ITransferLimitAdjRepository _tlAdjRepo;

        public TransferLimitRepository(
            IRawMaterialRepository rawMatRepo,
            ITransferLimitAdjRepository tlAdjRepo)
        {
            _rawMatRepo = rawMatRepo;
            _tlAdjRepo = tlAdjRepo;
        }

        public TransferLimitViewModel Get(long id)
        {
            var model = new TransferLimitViewModel()
            {
                TransferLimitId = id
            };

            var parameters = initParameters(model);
            StringBuilder qry = new StringBuilder();

            qry.AppendLine("Select top 1 * from TransferLimitViews");
            qry.AppendLine($"Where {nameof(TransferLimit.TransferLimitId)} = {nameof(TransferLimit.TransferLimitId).Parameterize()}");
            var result = DBContext.GetRecord<TransferLimitViewModel>(qry.ToString(), parameters);
            return result;
        }

        public TransferLimitViewModel Get(TransferLimitViewModel model)
        {
            var parameters = initParameters(model);
            StringBuilder qry = new StringBuilder();

            qry.AppendLine("Select top 1 * from TransferLimitViews");
            qry.AppendLine($"Where {nameof(TransferLimit.EffectiveDate)} = {nameof(TransferLimit.EffectiveDate).Parameterize()}");
            qry.AppendLine($"and {nameof(TransferLimit.RawMaterialId)} = {nameof(TransferLimit.RawMaterialId).Parameterize()}");
            qry.AppendLine($"and {nameof(TransferLimit.ShiftId)} = {nameof(TransferLimit.ShiftId).Parameterize()}");
            var result = DBContext.GetRecord<TransferLimitViewModel>(qry.ToString(), parameters);
            return result;
        }

        public TransferLimitViewModel2 GetStatus(TransferLimitViewModel model)
        {
            var parameters = initParameters(model);
            StringBuilder qry = new StringBuilder();

            qry.AppendLine("Select top 1 * from TransferLimitViews2");
            qry.AppendLine($"Where {nameof(TransferLimit.EffectiveDate)} = {nameof(TransferLimit.EffectiveDate).Parameterize()}");
            qry.AppendLine($"and {nameof(TransferLimit.RawMaterialId)} = {nameof(TransferLimit.RawMaterialId).Parameterize()}");
            qry.AppendLine($"and {nameof(TransferLimit.ShiftId)} = {nameof(TransferLimit.ShiftId).Parameterize()}");
            var result = DBContext.GetRecord<TransferLimitViewModel2>(qry.ToString(), parameters);
            return result;
        }

        public TransferLimitData GetTransferLimitData(TransferLimitViewModel model)
        {
            var tLimit = Get(model);
            if (tLimit == null)
            {
                return null;
            }

            var tLimitAdjs = _tlAdjRepo.List(tLimit.TransferLimitId);

            var result = new TransferLimitData()
            {
                TransferLimitViewModel = tLimit,
                TransferLimitAdjCollection = tLimitAdjs
            };

            return result;
        }
       
        public IEnumerable<TransferLimitViewModel> List(string qry = "")
        {
            if (qry == string.Empty) qry = "Select * from TransferLimitViews";
            var results = DBContext.GetRecords<TransferLimitViewModel>(qry.ToString()).ToList();
            return results;
        }

        public IEnumerable<TransferLimitViewModel> ListByEffectiveDate(DateTime? dt)
        {
           
            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.EffectiveDate).Parameterize(), ParameterValue = (dt ?? DateTime.Now).Date, ParameterDbType = System.Data.DbType.Date });

            var qry = new StringBuilder();
            qry.AppendLine($"Select * from TransferLimitViews");
            if (dt.HasValue) qry.AppendLine($"where {nameof(TransferLimit.EffectiveDate)} = {nameof(TransferLimit.EffectiveDate).Parameterize()}");
            qry.AppendLine($" Order by {nameof(TransferLimitViewModel.RawMaterialDesc)} asc");
            var results = DBContext.GetRecords<TransferLimitViewModel>(qry.ToString(),parameters).ToList();
            return results;
        }

        public IEnumerable<TransferLimitViewModel2> ListByEffectiveDate2(DateTime? dt)
        {

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.EffectiveDate).Parameterize(), ParameterValue = (dt ?? DateTime.Now).Date, ParameterDbType = System.Data.DbType.Date });

            var qry = new StringBuilder();
            qry.AppendLine($"Select * from TransferLimitViews2");
            if (dt.HasValue) qry.AppendLine($"where {nameof(TransferLimit.EffectiveDate)} = {nameof(TransferLimit.EffectiveDate).Parameterize()}");
            var results = DBContext.GetRecords<TransferLimitViewModel2>(qry.ToString(), parameters).ToList();
            return results;
        }

        public TransferLimitViewModel Update(TransferLimitViewModel modelChanges)
        {
            var parameters = initParameters(modelChanges);

            StringBuilder qry = new StringBuilder();
            qry.AppendLine("  declare @LimitWarningPercent decimal; set @LimitWarningPercent = (Select top 1 LimitWarningPercent from GeneralSettings)   ");
            qry.AppendLine("Update TransferLimits set");
            qry.AppendLine($"{nameof(modelChanges.ShiftId)}={nameof(modelChanges.ShiftId).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(modelChanges.RawMaterialId)}={nameof(modelChanges.RawMaterialId).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(modelChanges.ComputedLimitKg)}={nameof(modelChanges.ComputedLimitKg).Parameterize()}" + (char)44);
            qry.AppendLine($"{nameof(modelChanges.Modified)}={nameof(modelChanges.Modified).Parameterize()}");

            qry.AppendLine($"where {nameof(modelChanges.TransferLimitId)} = {nameof(modelChanges.TransferLimitId).Parameterize()}");
            int success = DBContext.ExecuteQuery(qry.ToString(), parameters);
            return modelChanges;
        }

        public TransferLimitViewModel Create(TransferLimitViewModel model)
        {

            var parameters = initParameters(model);

            StringBuilder qry = new StringBuilder();


            qry.AppendLine("insert into TransferLimits (");
            qry.AppendLine(nameof(model.DateCreated) + (char)44);
            qry.AppendLine(nameof(model.EffectiveDate) + (char)44);
            qry.AppendLine(nameof(model.ShiftId) + (char)44);
            qry.AppendLine(nameof(model.RawMaterialId) + (char)44);
            qry.AppendLine(nameof(model.ComputedLimitKg) + (char)44);
            qry.AppendLine(nameof(model.CreatedById));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(model.DateCreated).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.EffectiveDate).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.ShiftId).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.RawMaterialId).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.ComputedLimitKg).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.CreatedById).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            model.TransferLimitId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);
            return model;
        }

        public void Delete(long[] ids)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine(string.Format("Delete from TransferLimits where TransferLimitId in  ({0})", strIds));
            int success = DBContext.ExecuteQuery(qry.ToString());
        }

        public TransferLimitViewModel CreateIfNotExists(TransferLimitViewModel model)
        {
            var parameters = initParameters(model);
            var qry = new StringBuilder();
            qry.AppendLine(" IF NOT EXISTS(Select top 1 * from TransferLimitViews  ");
            qry.AppendLine(" WHERE EffectiveDate = @EffectiveDate ");
            qry.AppendLine(" AND RawMaterialId = @RawMaterialId ");
            qry.AppendLine(" AND ShiftId = @ShiftId)  ");
            qry.AppendLine("BEGIN");
            qry.AppendLine("insert into TransferLimits (");
            qry.AppendLine(nameof(model.DateCreated) + (char)44);
            qry.AppendLine(nameof(model.EffectiveDate) + (char)44);
            qry.AppendLine(nameof(model.ShiftId) + (char)44);
            qry.AppendLine(nameof(model.RawMaterialId) + (char)44);
            qry.AppendLine(nameof(model.ComputedLimitKg) + (char)44);
            qry.AppendLine(nameof(model.CreatedById));
            qry.AppendLine(") values (");
            qry.AppendLine(nameof(model.DateCreated).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.EffectiveDate).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.ShiftId).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.RawMaterialId).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.ComputedLimitKg).Parameterize() + (char)44);
            qry.AppendLine(nameof(model.CreatedById).Parameterize());
            qry.AppendLine(")");
            qry.AppendLine("select @@identity");
            qry.AppendLine("END");
            model.TransferLimitId = DBContext.ExecuteQueryWithIdentityInt64(qry.ToString(), parameters);

            return model;
        }

        public TransferLimitViewModel CheckLimit(TransferLimitViewModel model)
        {

            if (model.ShiftId == 2)
            {
                var efTime = model.EffectiveDate.Value.TimeOfDay;

                if (efTime >= new TimeSpan(0,0,0) && efTime <= new TimeSpan(6,59,59))
                {
                    model.EffectiveDate = model.EffectiveDate.Value.AddDays(-1);
                }
            }
    
            var parameters = initParameters(model);

            var str = new StringBuilder();
            str.AppendLine("   select top 1 * from TransferLimitViews as TL where    ");
            str.AppendLine("  				 TL.RawMaterialId = @RawMaterialId and ");
            str.AppendLine("  				 TL.EffectiveDate = @EffectiveDate and ");
            str.AppendLine("  					TL.ShiftId = @ShiftId");
            str.AppendLine("  ");

            var result = DBContext.GetRecord<TransferLimitViewModel>(str.ToString(),parameters);
            return result;

        }

        private List<ParameterInfo> initParameters(TransferLimit model)
        {
            model.DateCreated = model.DateCreated ?? DateTime.Now;
            model.EffectiveDate = model.EffectiveDate.Value.Date;
            model.BinNum = model.BinNum ?? string.Empty;

            //var limitWarningPercent = genRepo.Get().LimitWarningPercent;
            //var selectedLimitKg = model.AdjComputedLimigKg == 0 ? model.ComputedLimitKg : model.AdjComputedLimigKg;
            //model.LimitWarningKg = selectedLimitKg - (selectedLimitKg * (limitWarningPercent * .01m));

            var parameters = new List<ParameterInfo>();
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.TransferLimitId).Parameterize(), ParameterValue = model.TransferLimitId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.DateCreated).Parameterize(), ParameterValue = model.DateCreated,ParameterDbType=System.Data.DbType.Date });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.EffectiveDate).Parameterize(), ParameterValue = model.EffectiveDate, ParameterDbType = System.Data.DbType.Date });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.ShiftId).Parameterize(), ParameterValue = model.ShiftId});
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.RawMaterialId).Parameterize(), ParameterValue = model.RawMaterialId });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.BinNum).Parameterize(), ParameterValue = model.BinNum });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.ComputedLimitKg).Parameterize(), ParameterValue = model.ComputedLimitKg });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.LimitWarningKg).Parameterize(), ParameterValue = model.LimitWarningKg });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.CreatedById).Parameterize(), ParameterValue = model.CreatedById });
            parameters.Add(new ParameterInfo() { ParameterName = nameof(TransferLimit.Modified).Parameterize(), ParameterValue = model.Modified});

            return parameters;
        }

        public void AutoGenTransferLimit(DateTime dt,string userId)
        {
            var mats = _rawMatRepo.List();
            StringBuilder qry = new StringBuilder();
            foreach(var mat in mats)
            {
                var tLimit = new TransferLimitViewModel();
                tLimit.DateCreated = DateTime.Now;
                tLimit.EffectiveDate = dt;
                tLimit.ShiftId = 1;
                tLimit.RawMaterialId = mat.RawMaterialId;
                tLimit.ComputedLimitKg = 0;
                tLimit.CreatedBy = userId;
                Create(tLimit);

                tLimit.ShiftId = 2;
                Create(tLimit);
            }
        }

    }
}