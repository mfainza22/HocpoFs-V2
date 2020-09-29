using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCore.Models;
using WeighingSystemCoreHelpers.Attributes.Validations;
using WeighingSystemCoreHelpers.Enums;

namespace WeighingSystemCore.ViewModels
{
    public class TransRecordViewModel : TransRecord
    {
        public TransRecordViewModel()
        {

        }

        [DisplayName("Gross Wt.")]
        public decimal GrossWt { get; set; }

        [DisplayName("Tare Wt.")]
        public decimal TareWt { get; set; }

        public string RawMaterialDesc { get; set; }


        public string BinLocDesc { get; set; }


        public string LocationName { get; set; }

        public string WarehouseName { get; set; }

        public string AreaDesc { get; set; }

        [DisplayName("Shift")]
        public string ShiftDesc { get; set; }

        public string PackagingTypeDesc { get; set; }
        [DisplayName("Weigh-In By")]
        public string WeigherInName { get; set; }

        [DisplayName("Weigh-Out By")]
        public string WeigherOutName { get; set; }

        public bool Submitted { get; set; }

        public string TransactionProcess { get; set; }
        public string TransactionStatus { get; set; }

        public string TicketType { get; set; }
        public bool IsOffline { get; set; }
        public decimal OfflineWt { get; set; }
        public decimal OnlineWt { get; set; }
        public Nullable<DateTime> DTOfflineDate { get; set; }
        public Nullable<DateTime> DTOfflineTime { get; set; }
  
        public decimal TolMinActualWt { get; set; }

        public decimal TolMaxActualWt { get; set; }
    }
}