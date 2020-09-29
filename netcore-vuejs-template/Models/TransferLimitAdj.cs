using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class TransferLimitAdj
    {
        public TransferLimitAdj()
        {

        }

        [DisplayName("I.D.")]
        public long TransferLimitAdjId { get; set; }
      
        public long TransferLimitId { get; set; }

        [DisplayName("Date Created")]
        [Required(ErrorMessage = "Date Created is required.")]
        public DateTime? AdjDate { get; set; }


        [DisplayName("Adjusted Limit in KG.")]
        public decimal AdjLimit { get; set; }

        [DisplayName("Remarks")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [MaxLength(200, ErrorMessage = "Remarks must not exceed to 200 characters.")]
        public string AdjRemarks { get; set; }

        [DisplayName("Created By")]
        public string AdjCreatedById { get; set; }

        [DisplayName("Created By")]
        public string AdjCreatedBy { get; set; }
          
        public bool IsActive { get; set; }

    }
}