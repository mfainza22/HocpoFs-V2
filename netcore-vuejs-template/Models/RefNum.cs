using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class RefNum
    {
        public RefNum()
        {

        }

        [DisplayName("Raw Material I.D.")]
        public long RefNumId { get; set; }

        [DisplayName("Receipt Series Number")]
        [MaxLength(15, ErrorMessage = "Receipt Series Number must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Receipt Series Number must not contain an invalid characters.")]
        [Required(ErrorMessage = "Receipt Series Number is required.")]
        public string ReceiptSeriesNum { get; set; }

    }
}