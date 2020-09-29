using Dapper;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Attributes.Validations;
using WeighingSystemCoreHelpers.Enums;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Repositories
{
    public class TransRecordRepository : ITransRecordRepository
    {
        private readonly IPalletRepository palletRepository;
        private readonly IRefNumRepository refNumRepo;

        public TransRecordRepository(
          IRefNumRepository _refNumRepo,
          IPalletRepository _palletRepository)
        {
            refNumRepo = _refNumRepo;
            palletRepository = _palletRepository;
        }

        public IList<TransRecordViewModel> List(string qry = "")
        {
            var results = DBContext.GetRecords<TransRecordViewModel>(qry);
            return results;
        }

        public TransRecordViewModel Get(Int64 id, Enums.TransactionStatus transactionStatus)
        {
            var filter = new TransRecordFilter
            {
                TransactionId = id,
                TableName = "TransactionRecordViews",
                TransactionStatus = transactionStatus.ToString()
            };

            var result = DBContext.GetRecord<TransRecordViewModel>(filter.ToString());
            if (result != null) result.TransactionStatus = transactionStatus.ToString();


            return result;
        }

        public TransRecordViewModel WeighIn(TransRecordViewModel transRecord)
        {
            transRecord.DailyTransPrefix = WeighingSystemCoreHelpers.Helpers.MonthLetter.GetMonthLetter(DateTime.Now.Month) + DateTime.Now.ToString("dd");
            var parameters = InitParameters(transRecord);

            StringBuilder tranQuery = new StringBuilder();
            tranQuery.AppendLine("set @DailyCounter = (Select(Select count(*) from TransactionRecords as TR where (CONVERT(DATE, TR.DTInbound, 101)) = (CONVERT(DATE, @DTInbound, 101))) + 1)");
            tranQuery.AppendLine("insert into TransactionRecords (");
            tranQuery.AppendLine(nameof(transRecord.ReceiptNum) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DTInbound) + (char)44);
            ////qry.AppendLine(nameof(transRecord.DTOutbound) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ForkliftNum) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.PalletNum) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.RawMaterialId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.PackagingTypeId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtPerPackage) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.EmptyPackageWt) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.Quantity) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtPerPackageActual) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.BinLocationId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.BinNum) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.LocationId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WarehouseId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtInbound) + (char)44);
            //qry.AppendLine(nameof(transRecord.WtOutbound) + (char)44);
            //qry.AppendLine(nameof(transRecord.NetWt) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DriverName) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeigherInId) + (char)44);
            //qry.AppendLine(nameof(transRecord.WeigherOutId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.Remarks) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DailyCounter) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DailyTransPrefix) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeighingAreaId) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeightStatus) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.OfflineIn) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ShiftDate) + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ShiftId));
            tranQuery.AppendLine(") values (");
            tranQuery.AppendLine(nameof(transRecord.ReceiptNum).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DTInbound).Parameterize() + (char)44);
            //qry.AppendLine(nameof(transRecord.DTOutbound).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ForkliftNum).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.PalletNum).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.RawMaterialId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.PackagingTypeId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtPerPackage).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.EmptyPackageWt).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.Quantity).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtPerPackageActual).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.BinLocationId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.BinNum).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.LocationId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WarehouseId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WtInbound).Parameterize() + (char)44);
            //qry.AppendLine(nameof(transRecord.WtOutbound).Parameterize() + (char)44);
            //qry.AppendLine(nameof(transRecord.NetWt).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DriverName).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeigherInId).Parameterize() + (char)44);
            //qry.AppendLine(nameof(transRecord.WeigherOutId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.Remarks).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DailyCounter).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.DailyTransPrefix).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeighingAreaId).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.WeightStatus).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.OfflineIn).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ShiftDate).Parameterize() + (char)44);
            tranQuery.AppendLine(nameof(transRecord.ShiftId).Parameterize());
            tranQuery.AppendLine(")");
            tranQuery.AppendLine("select @@identity");

            using IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString());
            objConnection.Open();
            using var transaction = objConnection.BeginTransaction();
            try
            {
                var refnumQuery = refNumRepo.GetQuery();
                var dt = new DataTable();
                var reader = SqlMapper.ExecuteReader(objConnection, refnumQuery.Query.ToString(), null, commandType: CommandType.Text, transaction: transaction);
                dt.Load(reader);
                transRecord.ReceiptNum = Convert.ToString(dt.Rows[0].ItemArray[0]);
                parameters.Add(new ParameterInfo() { ParameterName = nameof(TransRecord.ReceiptNum).Parameterize(), ParameterValue = transRecord.ReceiptNum });
                dt.Dispose();

                dt = new DataTable();
                reader = null;
                reader = SqlMapper.ExecuteReader(objConnection, tranQuery.ToString(), parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);
                dt.Load(reader);
                transRecord.TransactionId = Convert.ToInt64(dt.Rows[0].ItemArray[0]);

                var tranRefNumUpdate = refNumRepo.GetUpdateQuery(new RefNum() { RefNumId = 1, ReceiptSeriesNum = transRecord.ReceiptNum });
                SqlMapper.Execute(objConnection, tranRefNumUpdate.Query.ToString(), tranRefNumUpdate.Parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);

                transaction.Commit();
                dt.Dispose();
                return transRecord;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                transaction.Rollback();
                throw new Exception(ex.Message);
            }

        }

        public TransRecordViewModel WeighOut(TransRecordViewModel transRecord)
        {
            transRecord.DTOutbound = transRecord.IsOffline ? transRecord.DTOfflineDate : DateTime.Now;
            transRecord.OfflineOut = transRecord.IsOffline;

            transRecord.WeightStatus = ToleranceCheck.CheckWtPerPackageActual(transRecord.TolActualWt,
            transRecord.WtPerPackageActual, transRecord.EmptyPackageWt, transRecord.WtPerPackage);

            var parameters = InitParameters(transRecord);

            StringBuilder tranQuery = new StringBuilder();

            tranQuery.AppendLine("update TransactionRecords set");
            tranQuery.AppendLine($"{nameof(transRecord.DTOutbound)}={nameof(transRecord.DTOutbound).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ForkliftNum)}={nameof(transRecord.ForkliftNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.PalletNum)}={nameof(transRecord.PalletNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.PackagingTypeId)}={nameof(transRecord.PackagingTypeId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WtPerPackage)}={nameof(transRecord.WtPerPackage).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.EmptyPackageWt)}={nameof(transRecord.EmptyPackageWt).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.RawMaterialId)}={nameof(transRecord.RawMaterialId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.Quantity)}={nameof(transRecord.Quantity).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WtPerPackageActual)}={nameof(transRecord.WtPerPackageActual).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.BinLocationId)}={nameof(transRecord.BinLocationId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.BinNum)}={nameof(transRecord.BinNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.LocationId)}={nameof(transRecord.LocationId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WarehouseId)}={nameof(transRecord.WarehouseId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WtOutbound)}={nameof(transRecord.WtOutbound).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.NetWt)}={nameof(transRecord.NetWt).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ActualNetWt)}={nameof(transRecord.ActualNetWt).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.DriverName)}={nameof(transRecord.DriverName).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WeigherOutId)}={nameof(transRecord.WeigherOutId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WeightStatus)}={nameof(transRecord.WeightStatus).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ShiftId)}={nameof(transRecord.ShiftId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ShiftDate)}={nameof(transRecord.ShiftDate).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.TransferLimitId)}={nameof(transRecord.TransferLimitId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.Remarks)}={nameof(transRecord.Remarks).Parameterize()}");
            tranQuery.AppendLine($" WHERE {nameof(transRecord.TransactionId)}={nameof(transRecord.TransactionId).Parameterize()}");

            var palletChanges = new Pallet()
            {
                PalletNum = transRecord.PalletNum,
                UpdatedWt = transRecord.WtOutbound
            };

            var palletQuery = palletRepository.CheckAndInsert(palletChanges);

            using IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString());
            objConnection.Open();
            using var transaction = objConnection.BeginTransaction();
            try
            {

                SqlMapper.Execute(objConnection, tranQuery.ToString(), parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);

                SqlMapper.Execute(objConnection, palletQuery.Query.ToString(), palletQuery.Parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);

                transaction.Commit();
                return transRecord;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public TransRecordViewModel Update(TransRecordViewModel transRecord, Enums.TransactionStatus transactionStatus)
        {
            transRecord.WeightStatus = ToleranceCheck.CheckWtPerPackageActual(transRecord.TolActualWt,
                transRecord.WtPerPackageActual, transRecord.EmptyPackageWt, transRecord.WtPerPackage);
            var parameters = InitParameters(transRecord);

            StringBuilder tranQuery = new StringBuilder();

            tranQuery.AppendLine("update TransactionRecords set");
            tranQuery.AppendLine($"{nameof(transRecord.ForkliftNum)}={nameof(transRecord.ForkliftNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.PalletNum)}={nameof(transRecord.PalletNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.PackagingTypeId)}={nameof(transRecord.PackagingTypeId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WtPerPackage)}={nameof(transRecord.WtPerPackage).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.EmptyPackageWt)}={nameof(transRecord.EmptyPackageWt).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WtPerPackageActual)}={nameof(transRecord.WtPerPackageActual).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.Quantity)}={nameof(transRecord.Quantity).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.RawMaterialId)}={nameof(transRecord.RawMaterialId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.BinLocationId)}={nameof(transRecord.BinLocationId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.BinNum)}={nameof(transRecord.BinNum).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.LocationId)}={nameof(transRecord.LocationId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WarehouseId)}={nameof(transRecord.WarehouseId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ActualNetWt)}={nameof(transRecord.ActualNetWt).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ModifiedById)}={nameof(transRecord.ModifiedById).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.ModifiedDate)}={nameof(transRecord.ModifiedDate).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.WeightStatus)}={nameof(transRecord.WeightStatus).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.TransferLimitId)}={nameof(transRecord.TransferLimitId).Parameterize()}" + (char)44);
            tranQuery.AppendLine($"{nameof(transRecord.Remarks)}={nameof(transRecord.Remarks).Parameterize()}");
            tranQuery.AppendLine($" WHERE {nameof(transRecord.TransactionId)}={nameof(transRecord.TransactionId).Parameterize()}");
            if (transactionStatus == Enums.TransactionStatus.PENDING)
            {
                tranQuery.AppendLine($" AND {nameof(transRecord.DTOutbound)} IS NULL");
            }
            else if (transactionStatus == Enums.TransactionStatus.COMPLETED)
            {
                tranQuery.AppendLine($" AND {nameof(transRecord.DTOutbound)} IS NOT NULL");
            }

            using IDbConnection objConnection = new SqlConnection(ServerUtils.ConnectionString());
            objConnection.Open();
            using var transaction = objConnection.BeginTransaction();
            try
            {
                SqlMapper.Execute(objConnection, tranQuery.ToString(), parameters.ToDynamicParams(), commandType: CommandType.Text, transaction: transaction);
                transaction.Commit();
                return transRecord;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void Delete(string[] ids, Enums.TransactionStatus tStatus)
        {
            string strIds = string.Format("'{0}'", string.Join("','", ids));
            StringBuilder qry = new StringBuilder();
            qry.AppendLine($"Delete from TransactionRecords where {nameof(TransRecord.TransactionId)} in ({strIds})");

            if (tStatus == Enums.TransactionStatus.PENDING)
            {
                qry.AppendLine(string.Format("and DTOutbound is null"));
            }
            else if (tStatus == Enums.TransactionStatus.COMPLETED)
            {
                qry.AppendLine(string.Format("and DTOutbound is not null"));
            } 

            DBContext.ExecuteQuery(qry.ToString());
        }

        private List<ParameterInfo> InitParameters(TransRecordViewModel transRecord)
        {
            if (string.IsNullOrEmpty(transRecord.WeigherInId)) transRecord.WeigherInId = "cea78691-84a0-411b-b29d-89b17d8e4c69";
            if (string.IsNullOrEmpty(transRecord.WeigherOutId)) transRecord.WeigherInId = "cea78691-84a0-411b-b29d-89b17d8e4c69";

            var parameters = new List<ParameterInfo>
            {
                new ParameterInfo() { ParameterName = nameof(TransRecord.TransactionId).Parameterize(), ParameterValue = transRecord.TransactionId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ReceiptNum).Parameterize(), ParameterValue = (transRecord.ReceiptNum ?? String.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.DTInbound).Parameterize(), ParameterValue = transRecord.DTInbound??DateTime.Now },
                new ParameterInfo() { ParameterName = nameof(TransRecord.DTOutbound).Parameterize(), ParameterValue = transRecord.DTOutbound },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ForkliftNum).Parameterize(), ParameterValue = (transRecord.ForkliftNum ?? string.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.PalletNum).Parameterize(), ParameterValue = (transRecord.PalletNum ?? string.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.RawMaterialId).Parameterize(), ParameterValue = transRecord.RawMaterialId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.PackagingTypeId).Parameterize(), ParameterValue = transRecord.PackagingTypeId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WtPerPackage).Parameterize(), ParameterValue = transRecord.WtPerPackage },
                new ParameterInfo() { ParameterName = nameof(TransRecord.EmptyPackageWt).Parameterize(), ParameterValue = transRecord.EmptyPackageWt },
                new ParameterInfo() { ParameterName = nameof(TransRecord.Quantity).Parameterize(), ParameterValue = transRecord.Quantity },
                new ParameterInfo() { ParameterName = nameof(TransRecord.EmptyPackageWt).Parameterize(), ParameterValue = transRecord.EmptyPackageWt },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WtPerPackageActual).Parameterize(), ParameterValue = transRecord.WtPerPackageActual },
                new ParameterInfo() { ParameterName = nameof(TransRecord.BinLocationId).Parameterize(), ParameterValue = transRecord.BinLocationId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.BinNum).Parameterize(), ParameterValue = (transRecord.BinNum ?? string.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.LocationId).Parameterize(), ParameterValue = transRecord.LocationId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WarehouseId).Parameterize(), ParameterValue = transRecord.WarehouseId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WtInbound).Parameterize(), ParameterValue = transRecord.WtInbound },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WtOutbound).Parameterize(), ParameterValue = transRecord.WtOutbound },
                new ParameterInfo() { ParameterName = nameof(TransRecord.NetWt).Parameterize(), ParameterValue = transRecord.NetWt },
                 new ParameterInfo() { ParameterName = nameof(TransRecord.ActualNetWt).Parameterize(), ParameterValue = transRecord.ActualNetWt },
                new ParameterInfo() { ParameterName = nameof(TransRecord.DriverName).Parameterize(), ParameterValue = (transRecord.DriverName ?? string.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WeigherInId).Parameterize(), ParameterValue = transRecord.WeigherInId??"" },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WeigherOutId).Parameterize(), ParameterValue = transRecord.WeigherOutId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ShiftId).Parameterize(), ParameterValue = transRecord.ShiftId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ShiftDate).Parameterize(), ParameterValue = transRecord.ShiftDate },
                new ParameterInfo() { ParameterName = nameof(TransRecord.Remarks).Parameterize(), ParameterValue = (transRecord.Remarks ?? string.Empty).ToUpper() },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ModifiedById).Parameterize(), ParameterValue = transRecord.ModifiedById },
                new ParameterInfo() { ParameterName = nameof(TransRecord.ModifiedDate).Parameterize(), ParameterValue = transRecord.ModifiedDate },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WeighingAreaId).Parameterize(), ParameterValue = transRecord.WeighingAreaId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.DailyCounter).Parameterize(), ParameterValue = transRecord.DailyCounter },
                new ParameterInfo() { ParameterName = nameof(TransRecord.DailyTransPrefix).Parameterize(), ParameterValue = transRecord.DailyTransPrefix },
                new ParameterInfo() { ParameterName = nameof(TransRecord.WeightStatus).Parameterize(), ParameterValue = transRecord.WeightStatus },
                new ParameterInfo() { ParameterName = nameof(TransRecord.TransferLimitId).Parameterize(), ParameterValue = transRecord.TransferLimitId },
                new ParameterInfo() { ParameterName = nameof(TransRecord.OfflineIn).Parameterize(), ParameterValue = transRecord.OfflineIn },
                new ParameterInfo() { ParameterName = nameof(TransRecord.OfflineOut).Parameterize(), ParameterValue = transRecord.OfflineOut }
            };
            return parameters;
        }

    }
}
