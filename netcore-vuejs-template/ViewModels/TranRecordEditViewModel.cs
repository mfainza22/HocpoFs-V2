using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCore.Models;
using WeighingSystemCoreHelpers.Attributes.Validations;
using WeighingSystemCoreHelpers.Enums;

namespace WeighingSystemCore.ViewModels
{
    public class TranRecordEditViewModel : TransRecord
    {
        public TranRecordEditViewModel()
        {

        }

        [DisplayName("Gross Wt.")]
        public decimal GrossWt { get; set; }

        [DisplayName("Tare Wt.")]
        public decimal TareWt { get; set; }

        public string RawMaterialDesc { get; set; }

        public string PackagingTypeDesc { get; set; }
        [DisplayName("Weigh-In By")]
        public string WeigherInName { get; set; }

        [DisplayName("Weigh-Out By")]
        public string WeigherOutName { get; set; }

        public bool Submitted { get; set; }

        public string TransactionProcess { get; set; }

        public string AspMethod
        {
            get
            {
                return "POST";
            }
        }
        public string AspAction
        {
            get
            {
                if (TransactionProcess == Enums.TransactionProcess.WEIGH_IN.ToString())
                {
                    return "WeighIn";
                }
                else if (TransactionProcess == Enums.TransactionProcess.WEIGH_OUT.ToString())
                {
                    return "WeighOut";
                }
                else if (TransactionProcess == Enums.TransactionProcess.UPDATE_IN.ToString() ||
                    TransactionProcess == Enums.TransactionProcess.UPDATE_OUT.ToString())
                {
                    return "Update";
                }
                else
                {
                    return "";
                }
            }
        }
        public string StatusMessage
        {
            get
            {
                if (TransactionProcess == Enums.TransactionProcess.WEIGH_IN.ToString())
                {
                    return "Weigh-In Complete";
                }
                else if (TransactionProcess == Enums.TransactionProcess.WEIGH_OUT.ToString())
                {
                    return "Weigh-Out Complete";
                }
                else if (TransactionProcess == Enums.TransactionProcess.UPDATE_IN.ToString() ||
                   TransactionProcess == Enums.TransactionProcess.UPDATE_OUT.ToString())
                {
                    return "Update Complete";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}