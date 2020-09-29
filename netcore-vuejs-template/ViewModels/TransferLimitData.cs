using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighingSystemCore.Models;

namespace WeighingSystemCore.ViewModels
{
    public class TransferLimitData
    {
        public TransferLimitViewModel TransferLimitViewModel { get; set; }

        //public TransferLimitStatus TransferLimitStatus { get; set; }

        public IEnumerable<TransferLimitAdj> TransferLimitAdjCollection { get; set; }
    }
}
