using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighingSystemCore.Models;

namespace WeighingSystemCore.ViewModels
{
    public class ReportingViewModel
    {
        public TranSummaryFilter Filter { get; set; }

        public List<GeneralSummaryViewModel> GenSummary { get; set; }

        public List<TranSummaryViewModel> TranSummary { get; set; }

        public List<TranSummaryViewModel2> TranSummary2 { get; set; }
    }
}
