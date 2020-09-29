using System;
using System.Text;
using WeighingSystemCoreHelpers.Enums;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Models
{
    public class TransRecordFilter
    {
        public TransRecordFilter()
        {
            Deleted = false;
            TableName = "TransactionRecordViews";
            SelectField = "*";
            FilterInboundDate = false;
            FilterOutboundDate = false;
            DTInboundFrom = DateTime.Now;
            DTInboundTo = DateTime.Now;
            DTOutboundFrom = DateTime.Now;
            DTOutboundTo = DateTime.Now;
        }
        public string TableName { get; set; }
        public string SelectField { get; set; }
        public bool FilterInboundDate { get; set; }
        public bool FilterOutboundDate { get; set; }
        public DateTime? DTInboundFrom { get; set; }
        public DateTime? DTInboundTo { get; set; }
        public DateTime? DTOutboundFrom { get; set; }
        public DateTime? DTOutboundTo { get; set; }
        public string TransactionStatus { get; set; }
        public long TransactionId { get; set; }
        public string ReceiptNum { get; set; }
        public long[] RawMaterialId { get; set; }
        public string RawMaterialDesc { get; set; }
        public string ForkliftNum { get; set; }
        public string PalletNum { get; set; }
        public long[] BinLocationId { get; set; }
        public string BinNum { get; set; }
        public string WarehouseName { get; set; }

        public long PackagingTypeId { get; set; }

        public long WeighingAreaId { get; set; }

        public Nullable<Boolean> Deleted { get; set; }

        public long ShiftId { get; set; }

        public string OrderByClause { get; set; }

        public override string ToString()
        {
            var filter = this;
            StringBuilder str = new StringBuilder();

            str.AppendLine($"Select {filter.SelectField}  from {filter.TableName} ");
            
            if (filter.TableName != "TranSummaryViews") {
                str.AppendLine("where TransactionId is not null");
            }

            if (filter == null) return str.ToString();

            if (!filter.TransactionId.IsNullOrZero())
            {
                str.AppendLine($"and {nameof(filter.TransactionId)} = '{filter.TransactionId}'");
            }

            if (!filter.ForkliftNum.IsNull())
            {
                str.AppendLine($"and {nameof(filter.ForkliftNum)} LIKE '%{filter.ForkliftNum}%'");
            }

            if (filter.FilterInboundDate)
            {
                if (filter.DTInboundFrom.HasValue) filter.DTInboundFrom = new DateTime(filter.DTInboundFrom.Value.Year, filter.DTInboundFrom.Value.Month, filter.DTInboundFrom.Value.Day);
                if (filter.DTInboundTo.HasValue) filter.DTInboundTo = new DateTime(filter.DTInboundTo.Value.Year, filter.DTInboundTo.Value.Month, filter.DTInboundTo.Value.Day) + new TimeSpan(23, 59, 59);

                string dateTypeField = "DTInbound";
                str.AppendLine($"and {dateTypeField} between '{filter.DTInboundFrom.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}' and '{filter.DTInboundTo.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}'");
            }

            if (TransactionStatus == Enums.TransactionStatus.PENDING.ToString())
            {
                str.AppendLine($"and {nameof(TransRecord.DTOutbound)} is null");

            } else if (TransactionStatus == Enums.TransactionStatus.COMPLETED.ToString())
            {
                str.AppendLine($"and {nameof(TransRecord.DTOutbound)} is not null");
            } 

            if (filter.FilterOutboundDate)
            {
                if (filter.DTOutboundFrom.HasValue) filter.DTOutboundFrom = new DateTime(filter.DTOutboundFrom.Value.Year, filter.DTOutboundFrom.Value.Month, filter.DTOutboundFrom.Value.Day);
                if (filter.DTOutboundTo.HasValue) filter.DTOutboundTo = new DateTime(filter.DTOutboundTo.Value.Year, filter.DTOutboundTo.Value.Month, filter.DTOutboundTo.Value.Day) + new TimeSpan(23, 59, 59);

                string dateTypeField = "DTOutbound";
                str.AppendLine($"and {dateTypeField} between '{filter.DTOutboundFrom.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}' and '{filter.DTOutboundTo.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}'");
            }

            if (!filter.ShiftId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.ShiftId)} = '{filter.ShiftId}'");

            if (!filter.ReceiptNum.IsNull()) str.AppendLine($"and {nameof(filter.ReceiptNum)} = '{filter.ReceiptNum}'");

            if (filter.RawMaterialId?.Length > 0)
            {
                str.AppendLine($"and {nameof(filter.RawMaterialId)} in ({String.Join(",",RawMaterialId)})");
            }

            if (!filter.ForkliftNum.IsNull()) str.AppendLine($"and {nameof(filter.ForkliftNum)} = '{filter.ForkliftNum}'");

            if (!filter.PalletNum.IsNull()) str.AppendLine($"and {nameof(filter.PalletNum)} = '{filter.PalletNum}'");

            if (!filter.BinNum.IsNull()) str.AppendLine($"and {nameof(filter.BinNum)} = '{filter.BinNum}'");

            if (!filter.PackagingTypeId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.PackagingTypeId)} = '{filter.PackagingTypeId}'");

            if (filter.BinLocationId?.Length > 0)
            {
                str.AppendLine($"and {nameof(filter.BinLocationId)} in ({String.Join(",", BinLocationId)})");
            }

            if (!filter.WarehouseName.IsNull()) str.AppendLine($"and {nameof(filter.WarehouseName)} = '{filter.WarehouseName}'");

            if (!filter.WeighingAreaId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.WeighingAreaId)} = '{filter.WeighingAreaId}'");

            if (!filter.Deleted.IsNull())
            {
                str.AppendLine($"and {nameof(filter.Deleted)} = '{filter.Deleted}'");
            }

            if (!this.OrderByClause.IsNull())
            {
                str.AppendLine(this.OrderByClause);
            }
            return str.ToString();
        }

    }

}
