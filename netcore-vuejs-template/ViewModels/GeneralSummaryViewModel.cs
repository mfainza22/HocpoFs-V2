using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingSystemCore.ViewModels
{
    public class GeneralSummaryViewModel
    {
        public long id { get; set; }

        public DateTime DTInbound { get; set; }

        public DateTime DTOutbound { get; set; }

        public DateTime ShiftDate { get; set; }

        public string BinNum { get; set; }

        public string ControlNum { get; set; }

        public long RawMaterialId { get; set; }

        public string RawMaterialDesc { get; set; }

        public string LocationName { get; set; }

        public string WarehouseName { get; set; }

        public string BinLocDesc { get; set; }

        public string PalletNum { get; set; }

        public decimal TotEmptyPackWt
        { 
            get {
                return NetWt - ActualNetWt;
            }
        }

        public decimal GrossWt { get; set; }
        public decimal NetWt { get; set; }
        public decimal ActualNetWt { get; set; }

        public string WeigherOutName { get; set; }

        public string DischargeTime
        {
            //get
            //{
            //    if (DTOutbound == null) return "--";
            //    return DTOutbound.ToString("HH:mm");
            //}
            get;set;
        }

    }
}
