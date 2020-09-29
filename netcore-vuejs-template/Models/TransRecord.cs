using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Attributes.Json;
using WeighingSystemCoreHelpers.Attributes.Validations;
using WeighingSystemCoreHelpers.Enums;

namespace WeighingSystemCore.Models
{

    public class TransRecord
    {
        public TransRecord()
        {

        }

        [Key]
        [DisplayName("Transaction I.D.")]
        public long TransactionId { get; set; }

        [DisplayName("Receipt Num.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "* Receipt Number must not contain an invalid characters.")]
        public string ReceiptNum { get; set;
        }
        [DisplayName("Weigh-In Date")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateTimeFormat), "MMM-dd-yyyy HH:mm:ss")]
        public Nullable<DateTime> DTInbound { get; set; }

        [DisplayName("Weigh-Out Date")]
        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateTimeFormat), "MMM-dd-yyyy HH:mm:ss")]
        public Nullable<DateTime> DTOutbound { get; set; }

        [DisplayName("Forklift Num.")]
        [MaxLength(20, ErrorMessage = "* Forklift Number must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "* Forklift number must not contain an invalid characters.")]
        //[Required(ErrorMessage = "Forklift number is required.")]
        public string ForkliftNum { get; set; }

        [DisplayName("Raw Material")]
        [ExistingValidator(Existing = false, ErrorMessage = "* Raw material does not exists.", PropertyTableName = "RawMaterials")]
        public long RawMaterialId { get; set; }

        [DisplayName("Pallet Number")]
        [MaxLength(20, ErrorMessage = "* Pallet number must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "* Pallet number must not contain an invalid characters.")]
        [Required(ErrorMessage = "Pallet number is required.")]
        public string PalletNum { get; set; }

        //[Range(0.01, 1000, ErrorMessage = "Pallet Weight must be between {1} to {2} Kg.")]
        [DisplayName("Pallet Wt.")]
        public decimal PalletWt { get; set; }

        [DisplayName("Packaging Type")]
        [ExistingValidator(Existing = false, ErrorMessage = "* Packaging Type does not exists.", PropertyTableName = "PackagingTypes")]
        public long PackagingTypeId { get; set; }

        //[Range(0.01, 1000, ErrorMessage = "Wt. Per pacakage is invalid.")]
        [DisplayName("Wt per Pack(Kg)")]
        public decimal WtPerPackage { get; set; }


        //[Range(0.01, 1000, ErrorMessage = "Empty Pack. Wt. is invalid.")]
        [DisplayName("Empty Pack. Wt(Kg)")]
        public decimal EmptyPackageWt { get; set; }

        [DisplayName("Tot. Empty Pack. Wt(Kg)")]
        public decimal TotalEmptyPackWt { get; set; }

        //[Range(0, 1000, ErrorMessage = "Actual Pack. Wt. must be between {1} to {2} Kg.")]
        [DisplayName("Actual Wt. Per Pack")]
        [ToleranceCheck(ErrorMessage ="* Actual Wt. Per Pack is invalid.",
            PropertyTolFieldName = nameof(TolActualWt),
            PropertyEmptyPackFieldName =nameof(EmptyPackageWt),
            PropertyWtPerPackFieldName = nameof(WtPerPackage),
            AllowZero = false)]
        public decimal WtPerPackageActual { get; set; }

        //[Range(0, 10000, ErrorMessage = "Quantity is Invalid")]
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; }

        [DisplayName("Bin Location")]
        [TransactionCustomRequired(ErrorMessage = "* Bin Location is required.",
            TransactionProcessFieldName = nameof(TransRecordViewModel.TransactionProcess),
            AcceptableTransactionProc = "WEIGH_IN,UPDATE_IN")]
        public long BinLocationId{ get; set; }

        [DisplayName("Bin #")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "* Bin number must not contain an invalid characters.")]
        //[TransactionCustomRequired(ErrorMessage = "* Bin number is required.",
        //    TransactionProcessFieldName =nameof(TranRecordViewModel.TransactionProcess),
        //    AcceptableTransactionProc = "WEIGH_IN,UPDATE_IN")]
        [MaxLength(5, ErrorMessage = "* Bin number must not exceed to 5 characters.")]
        public string BinNum { get; set; }

        [DisplayName("Location")]
        [TransactionCustomRequired(ErrorMessage = "* Location is required.",
            TransactionProcessFieldName = nameof(TransRecordViewModel.TransactionProcess),
            AcceptableTransactionProc = "WEIGH_IN,UPDATE_IN")]
        public long LocationId { get; set; }

        [DisplayName("Warehouse Name")]
        [TransactionCustomRequired(ErrorMessage = "* Warehouse is required.",
            TransactionProcessFieldName = nameof(TransRecordViewModel.TransactionProcess),
            AcceptableTransactionProc = "WEIGH_IN,UPDATE_IN")]
        public long WarehouseId { get; set; }

        [DisplayName("Weighing Area")]
        [ExistingValidator(Existing = false, ErrorMessage = "* Weighing area does not exists.", PropertyTableName = "WeighingAreas")]
        public long WeighingAreaId { get; set; }

        [DisplayName("Inbound Wt.")]
        public decimal WtInbound { get; set; }

        [DisplayName("Outbound Wt.")]
        public decimal WtOutbound { get; set; }

        [DisplayName("Net Wt.")]
        public decimal NetWt { get; set; }

        [DisplayName("Net Wt.")]
        public decimal ActualNetWt { get; set; }

        public string WeightStatus { get; set; }

        [DisplayName("Driver Name")]
        [MaxLength(70, ErrorMessage = "* Driver name  must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "* Driver name not contain an invalid characters.")]
        public string DriverName { get; set; }

        [DisplayName("Checker-In")]
        [MaxLength(100, ErrorMessage = "* Weigher In I.D. by must not exceed to {0} characters.")]
        public string WeigherInId { get; set; }

        [DisplayName("Checker-Out")]
        [MaxLength(100, ErrorMessage = "* Weigher Out I.D by must not exceed to {0} characters.")]
        public string WeigherOutId { get; set; }

        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd-yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateTimeFormat), "MMM-dd-yyyy HH:mm:ss")]
        public Nullable<DateTime> ModifiedDate { get; set; }

        [DisplayName("Modified By")]
        [MaxLength(100, ErrorMessage = "* Modified by must not exceed to {0} characters.")]
        public string ModifiedById { get; set; }

        [DisplayName("Shift")]
        public long ShiftId { get; set; }

        public DateTime? ShiftDate { get; set; }

        public int DailyCounter { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        public bool OfflineIn { get; set; }

        public bool OfflineOut { get; set; }

        public string ReceiptNumPrefix { get; set; }

        public string DailyTransPrefix { get; set; }

        public decimal TolActualWt { get; set; }

        public long TransferLimitId { get; set; }

        public string ControlNum { get; set; }

    }
}