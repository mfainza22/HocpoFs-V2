using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class Pallet
    {
        public Pallet()
        {

        }

        [DisplayName("Pallet Type I.D.")]
        public long PalletId { get; set; }

        [DisplayName("Pallet Number")]
        [MaxLength(20, ErrorMessage = "Pallet number must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Pallet number is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Pallet number must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Pallet number type already exists.", PropertyIdFieldName = nameof(PalletId), PropertyTableName = "Pallets")]
        public string PalletNum { get; set; }

        [DisplayName("Pallet Type")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Pallet type must not contain an invalid characters.")]
        public string PalletType { get; set; }

        [DisplayName("Pallet Type")]
        public long PalletTypeId { get; set; }

        [DisplayName("Last Recorded Wt. (Kg)")]
        [Range(0, 50000, ErrorMessage = "Weight must be between 0 to 50,000 Kg.")]
        public decimal UpdatedWt { get; set; }

        [DisplayName("Estimated Wt. (Kg)")]
        [Range(0, 50000, ErrorMessage = "Weight must be between 0 to 50,000 Kg.")]
        public decimal EstimatedWt { get; set; }



    }
}