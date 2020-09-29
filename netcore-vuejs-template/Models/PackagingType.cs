using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class PackagingType
    {
        public PackagingType()
        {

        }

        [DisplayName("Packaging Type I.D.")]
        public long PackagingTypeId { get; set; }

        [DisplayName("Packaging Type")]
        [MaxLength(20, ErrorMessage = "Packaging type must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Packaging type is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Packaging type must not contain an invalid characters.")]
        public string PackagingTypeDesc { get; set; }


        [DisplayName("Empty Package Type Wt. (Kg)")]
        //[Range(1, 200, ErrorMessage = "Empty Weight must be between 1 to 200 Kg.")]
        public decimal EmptyPackageWt { get; set; }


    }
}