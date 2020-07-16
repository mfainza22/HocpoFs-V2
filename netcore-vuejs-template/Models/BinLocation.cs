using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class BinLocation
    {
        public BinLocation()
        {

        }

        [DisplayName("Bin Location I.D.")]
        public long BinLocationId { get; set; }

        [DisplayName("Bin Location")]
        [MaxLength(20, ErrorMessage = "Bin location must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Bin  location is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Bin location already exists.", PropertyIdFieldName = nameof(BinLocationId), PropertyTableName = "BinLocations")]
        public string BinLocDesc { get; set; }


    }
}