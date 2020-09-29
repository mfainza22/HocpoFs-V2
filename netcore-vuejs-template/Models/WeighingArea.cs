using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class WeighingArea
    {
        public WeighingArea()
        {

        }

        [DisplayName("Weighing Area I.D.")]
        public long WeighingAreaId { get; set; }


        [MaxLength(100, ErrorMessage = "Description must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Description must not contain an invalid characters.")]
        public string AreaCode { get; set; }

        [MaxLength(100, ErrorMessage = "Description must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Description must not contain an invalid characters.")]
        public string AreaDesc { get; set; }

    }
}