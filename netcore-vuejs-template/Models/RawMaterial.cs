using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class RawMaterial
    {
        public RawMaterial()
        {

        }

        [DisplayName("Raw Material I.D.")]
        public long RawMaterialId { get; set; }

        [DisplayName("Code")]
        [MaxLength(20, ErrorMessage = "Description must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [Required(ErrorMessage = "Code is required.")]
        public string RawMaterialCode { get; set; }

        [DisplayName("Description")]
        [MaxLength(50, ErrorMessage = "Description must not exceed to {0} characters.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Description must not contain an invalid characters.")]
        [Required(ErrorMessage = "Description is required.")]
        public string RawMaterialDesc { get; set; }


    }
}