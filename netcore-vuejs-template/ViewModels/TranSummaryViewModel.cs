using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingSystemCore.ViewModels
{
    public class TranSummaryViewModel
    {
        public long id { get; set; }

        public DateTime DTInbound { get; set; }

        public DateTime DTOutbound { get; set; }
        public DateTime ShiftDate { get; set; }

        public string BinNum { get; set; }

        public string RawMaterialId { get; set; }

        public string RawMaterialDesc { get; set; }

        public string LocationName { get; set; }

        public string WarehouseName { get; set; }

        public string BinLocDesc { get; set; }

        public decimal QS1 { get; set; }

        public decimal QS2 { get; set; }

        public decimal QS3 { get; set; }

        public decimal NS1 { get; set; }

        public decimal NS2 { get; set; }

        public decimal NS3 { get; set; }

        public int PalletCount { get; set; }

        public long TransferLimitId { get; set; }

        public decimal ComputedLimitKg { get; set; }

        public decimal CurrentLimitKg { get; set; }

        public string AdjRemarks { get; set; }


    }
}
