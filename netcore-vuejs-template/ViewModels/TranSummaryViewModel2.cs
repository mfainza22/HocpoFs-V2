using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeighingSystemCore.Models;

namespace WeighingSystemCore.ViewModels
{
    public class TranSummaryViewModel2
    {
        public DateTime DTOutbound { get; set; }

        public DateTime ShiftDate { get; set; }

        public long RawMaterialId { get; set; }
        public string RawMaterialDesc { get; set; }

        public long ShiftId { get; set; }

        public string ShiftDesc { get; set; }

        public decimal Quantity { get; set; }

        public decimal ActualNetWt { get; set; }

        public decimal ComputedLimitKg { get; set; }

        public decimal AdjustedLimitKg { get; set; }

        public decimal CurrentLimitKg { get; set; }

        public string AdjRemarks { get; set; }
    }
}
