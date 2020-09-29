using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeighingSystemCore.Models;

namespace WeighingSystemCore.ViewModels
{
    public class TransferLimitViewModel : TransferLimit
    {
        public string RawMaterialDesc { get; set; }
        public long TransferLimitAdjId { get; set; }

        public string ShiftDesc {get;set;}
        public decimal AdjLimit { get; set; }
        public string AdjRemarks { get; set; }

        public string AdjCreatedBy { get; set; }

    }
}
