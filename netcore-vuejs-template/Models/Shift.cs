using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class Shift
    {
        public Shift()
        {

        }

        [DisplayName(" ")]
        public bool Selected { get; set; }

        [DisplayName("Group I.D.")]
        public long ShiftId { get; set; }

        [DisplayName("Code")]
        [MaxLength(20, ErrorMessage = "Code must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Code is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Code  already exists.", PropertyIdFieldName = nameof(ShiftId), PropertyTableName = "Shifts")]
        public string ShiftCode { get; set; }

        [DisplayName("Group Desc")]
        [MaxLength(50, ErrorMessage = "Description must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Description already exists.", PropertyIdFieldName = nameof(ShiftId), PropertyTableName = "Shifts")]
        public string ShiftDesc { get; set; }

        public string TimeFrom { get; set; }

        public string TimeTo { get; set; }

        public DateTime? ShiftDate { get; set; }


    }
}