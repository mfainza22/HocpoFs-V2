using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class GeneralSettings
    {
        public GeneralSettings()
        {

        }

        [DisplayName("Settings I.D.")]
        public long SettingsId { get; set; }

        [DisplayName("Tolerance(KG)")]
        [Range(0,100,ErrorMessage ="Tolerance must be a valid numeric value")]
        public decimal Tolerance { get; set; }

        [DisplayName("Computed Limig Warning (%)")]
        public decimal LimitWarningPercent { get; set; }


        [DisplayName("Client Code")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Client code must not contain an invalid characters.")]
        [Required(ErrorMessage = "Client code is required.")]
        [MaxLength(10, ErrorMessage = "Client code must not exceed to 10 characters.")]
        public string ClientCode { get; set; }


        [DisplayName("Client Name")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Client Name must not contain an invalid characters.")]
        [Required(ErrorMessage = "Client Name is required.")]
        [MaxLength(300, ErrorMessage = "Client Name must not exceed to 300 characters.")]
        public string ClientName { get; set; }


        [DisplayName("Client Address")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Client Address must not contain an invalid characters.")]
        [Required(ErrorMessage = "Client Address is required.")]
        [MaxLength(300, ErrorMessage = "Client Address must not exceed to 300 characters.")]
        public string ClientAddress { get; set; }

        public bool Submitted { get; set; }


    }
}