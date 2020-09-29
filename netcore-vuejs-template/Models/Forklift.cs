using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class Forklift
    {
        public Forklift()
        {

        }

        [DisplayName("Forklift I.D.")]
        public long ForkliftId { get; set; }

        [DisplayName("Forklift Number")]
        [MaxLength(20, ErrorMessage = "Forklift number must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Forklift number is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Forklift must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Forklift already exists.", PropertyIdFieldName = nameof(ForkliftId), PropertyTableName = "Forklifts")]
        public string ForkliftNum { get; set; }

        [DisplayName("Tare Wt.(KG)")]
        [Range(0, 10000, ErrorMessage = "Tare Weight must be between {0} to {1} Kg")]
        public decimal UpdatedTareWt { get; set; }


    }
}