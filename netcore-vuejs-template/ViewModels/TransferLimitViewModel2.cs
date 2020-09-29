using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeighingSystemCore.Models;

namespace WeighingSystemCore.ViewModels
{
    public class TransferLimitViewModel2
    {
        public DateTime EffectiveDate { get; set; }
        public long RawMaterialId { get; set; }

        public string RawMaterialDesc { get; set; }

        public decimal LimitShift1 { get; set; }

        public decimal LimitShift2 { get; set; }

        public string AdjRemarksShift1 { get; set; }
        public string AdjRemarksShift2 { get; set; }

        public int LimitStatusShift1 { get; set; }

        public int LimitStatusShift2 { get; set; }

        public bool ModifiedShift1 {get;set;}

        public bool ModifiedShift2 { get; set; }

    }
}
