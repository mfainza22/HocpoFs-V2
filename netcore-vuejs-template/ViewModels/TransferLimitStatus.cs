using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingSystemCore.ViewModels
{
    public class TransferLimitStatus : TransferLimitViewModel2
    {
        public long TransferLimitId { get; set; }

        public string ShiftDesc { get; set; }

        public decimal ComputedLimitKg { get; set; }

        public decimal CurrentLimitKg { get; set; }

        public decimal LimitWarningKg { get; set; }

        public decimal TransferredKg { get; set; }

        public int LimitStatus { get; set; }

    }
}
