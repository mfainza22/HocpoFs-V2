using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingSystemCore.ViewModels
{
    public class EditTransferLimitViewModel
    {
        public DateTime? EffectiveDate { get; set; }

        public long RawMaterialId { get; set; }

        public long ShiftId { get; set; }

        public decimal ComputedLimitKg { get; set; }
    }
}
