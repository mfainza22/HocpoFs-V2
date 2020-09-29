using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WeighingSystemCoreHelpers.Enums;
using WeighingSystemCoreHelpers.Extensions;

namespace WeighingSystemCore.Models
{
    public class TranSummaryFilter
    {
        public TranSummaryFilter()
        {
            Deleted = false;
            TableName = "TranSummaryViews";
            SelectField = "*";
            FilterInboundDate = false;
            FilterOutboundDate = false;
            FilterShiftDate = false;
            DTInboundFrom = DateTime.Now;
            DTInboundTo = DateTime.Now;
            DTOutboundFrom = DateTime.Now;
            DTOutboundTo = DateTime.Now;
            OrderBy = "Order by RawMaterialdesc,DTInbound,BinNum asc";
        }


        public string TableName { get; set; }
        public string SelectField { get; set; }
        public bool FilterInboundDate { get; set; }
        public bool FilterOutboundDate { get; set; }
        public bool FilterShiftDate { get; set; }
        public DateTime? DTInboundFrom { get; set; }
        public DateTime? DTInboundTo { get; set; }
        public DateTime? DTOutboundFrom { get; set; }
        public DateTime? DTOutboundTo { get; set; }

        [DisplayName("Shift Date")]
        public DateTime? ShiftDateFrom { get; set; }
        public DateTime? ShiftDateTo { get; set; }


        public string TransactionStatus { get; set; }

        [DisplayName("Raw Material")]
        public long RawMaterialId { get; set; }

        [DisplayName("Bin Location")]
        public long BinLocationId { get; set; }

        [DisplayName("Pallet Number")]
        public string PalletNum { get; set; }

        [DisplayName("Bin Number")]
        public string BinNum { get; set; }

        [DisplayName("Location")]
        public long LocationId { get; set; }

        [DisplayName("Warehouse")]
        public long WarehouseId { get; set; }

        [DisplayName("Weighing Area")]
        public long WeighingAreaId { get; set; }

        [DisplayName("Packaging Type")]
        public long PackagingTypeId { get; set; }

        public long ShiftId { get; set; }
  
        public Nullable<Boolean> Deleted { get; set; }

        public string OrderBy { get; set; }

        [DisplayName("Report Type")]
        [Required(ErrorMessage ="Report type is required.")]
        public int ReportTypeKey { get; set; }

        public override string ToString()
        {
            var filter = this;
            StringBuilder str = new StringBuilder();

            str.AppendLine($"Select {filter.SelectField}  from {filter.TableName} ");
            
            str.AppendLine("where RawMaterialDesc is not null");
        

            if (filter == null) return str.ToString();


            if (filter.FilterInboundDate)
            {
                if (filter.DTInboundFrom.HasValue) filter.DTInboundFrom = new DateTime(filter.DTInboundFrom.Value.Year, filter.DTInboundFrom.Value.Month, filter.DTInboundFrom.Value.Day);
                if (filter.DTInboundTo.HasValue) filter.DTInboundTo = new DateTime(filter.DTInboundTo.Value.Year, filter.DTInboundTo.Value.Month, filter.DTInboundTo.Value.Day) + new TimeSpan(23, 59, 59);

                string dateTypeField = "DTInbound";
                str.AppendLine($"and {dateTypeField} between '{filter.DTInboundFrom.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}' and '{filter.DTInboundTo.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}'");
            }


            if (filter.FilterOutboundDate)
            {
                if (filter.DTOutboundFrom.HasValue) filter.DTOutboundFrom = new DateTime(filter.DTOutboundFrom.Value.Year, filter.DTOutboundFrom.Value.Month, filter.DTOutboundFrom.Value.Day);
                if (filter.DTOutboundTo.HasValue) filter.DTOutboundTo = new DateTime(filter.DTOutboundTo.Value.Year, filter.DTOutboundTo.Value.Month, filter.DTOutboundTo.Value.Day) + new TimeSpan(23, 59, 59);

                string dateTypeField = "DTOutbound";
                str.AppendLine($"and {dateTypeField} between '{filter.DTOutboundFrom.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}' and '{filter.DTOutboundTo.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}'");
            }

            if (filter.FilterShiftDate)
            {
                if (filter.ShiftDateFrom.HasValue) filter.ShiftDateFrom = filter.ShiftDateFrom.Value.Date;
                if (filter.ShiftDateTo.HasValue) filter.ShiftDateTo = filter.ShiftDateTo.Value.Date + new TimeSpan(23, 59, 59);

                string dateTypeField = "ShiftDate";
                str.AppendLine($"and {dateTypeField} between '{filter.ShiftDateFrom.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}' and '{filter.ShiftDateTo.Value.ToString("yyyy-MMM-dd hh:mm:ss tt")}'");
            }

            if (!filter.RawMaterialId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.RawMaterialId)} = '{filter.RawMaterialId}'");

            if (!filter.PackagingTypeId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.PackagingTypeId)} = '{filter.PackagingTypeId}'");

            if (!filter.BinLocationId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.BinLocationId)} = '{filter.BinLocationId}'");

            if (!filter.PalletNum.IsNull()) str.AppendLine($"and {nameof(filter.PalletNum)} = '{filter.PalletNum}'");

            if (!filter.BinNum.IsNull()) str.AppendLine($"and {nameof(filter.BinNum)} = '{filter.BinNum}'");

            if (!filter.ShiftId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.ShiftId)} = '{filter.ShiftId}'");


            if (!filter.LocationId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.LocationId)} = '{filter.LocationId}'");

            if (!filter.WarehouseId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.WarehouseId)} = '{filter.WarehouseId}'");

            if (!filter.WeighingAreaId.IsNullOrZero()) str.AppendLine($"and {nameof(filter.WeighingAreaId)} = '{filter.WeighingAreaId}'");


            if (!filter.OrderBy.IsNull()) str.AppendLine(OrderBy);

            return str.ToString();
        }

    }

}
