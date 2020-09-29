using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Json;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class DailyTransPrefix
    {
        public DailyTransPrefix()
        {

        }

        [DisplayName("I.D.")]
        public long DailyTransPrefixId { get; set; }

        [DisplayName("Prefix")]
        [MaxLength(2, ErrorMessage = "Prefix must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Prefix is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
       
        public string Prefix { get; set; }

        [Required(ErrorMessage = "Prefix is required.")]
        [JsonConverter(typeof(JsonDateTimeFormat), "yyyy-MM-dd")]
        [ExistingValidator(ErrorMessage = "Effective Date already exists.", PropertyIdFieldName = nameof(DailyTransPrefixId), PropertyTableName = "DailyTransPrefixes")]
        public DateTime EffectiveDate { get; set; }


    }
}