using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class TransferLimit
    {
        public TransferLimit()
        {

        }

        [DisplayName("I.D.")]
        public long TransferLimitId { get; set; }

        [DisplayName("Date Created")]
        //[Required(ErrorMessage = "Date Created is required.")]
        public DateTime? DateCreated { get; set; }

        [DisplayName("Effective Date")]
        [Required(ErrorMessage = "Effective Date is required.")]
        public DateTime? EffectiveDate { get; set; }

        [DisplayName("Shift")]
        [Required(ErrorMessage = "Shift is required.")]
        public long ShiftId { get; set; }

        [DisplayName("Raw Material")]
        [Required(ErrorMessage = "Raw Material is required.")]
        public long RawMaterialId { get; set; }

        [DisplayName("Bin Number")]
        [MaxLength(2, ErrorMessage = "Bin Number must not exceed to 2 characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        public string BinNum { get; set; }

        [DisplayName("Computed Limit (KG)")]
        public decimal ComputedLimitKg { get; set; }

        [DisplayName("Adj. Limit (KG)")]
        public decimal AdjComputedLimigKg { get; set; }

        [DisplayName("Limit Warning (KG)")]
        public decimal LimitWarningKg { get; set; }

        [DisplayName("Transferred (KG)")]
        public decimal TransferredKg { get; set; }
        public string LimitStatus { get; set; }


        [DisplayName("Created By")]
        public string CreatedById { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        public bool Modified { get; set; }
    }
}