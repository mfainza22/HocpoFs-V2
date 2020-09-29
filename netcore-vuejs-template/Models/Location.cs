using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class Location
    {
        public Location()
        {

        }


        [DisplayName("Location I.D.")]
        public long LocationId { get; set; }

        [DisplayName("Location Name")]
        [MaxLength(20, ErrorMessage = "Location must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Location is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Location  already exists.", PropertyIdFieldName = nameof(LocationId), PropertyTableName = "Locations")]
        public string LocationName { get; set; }


    }
}