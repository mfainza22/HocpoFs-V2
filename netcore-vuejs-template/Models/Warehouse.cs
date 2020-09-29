using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;

namespace WeighingSystemCore.Models
{
    public class Warehouse
    {
        public Warehouse()
        {

        }

        [DisplayName(" ")]
        public bool Selected { get; set; }

        [DisplayName("Warehouse I.D.")]
        public long WarehouseId { get; set; }

        [DisplayName("Code")]
        [MaxLength(20, ErrorMessage = "Code must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Code is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Code  already exists.", PropertyIdFieldName = nameof(WarehouseId), PropertyTableName = "Warehouses")]
        public string WarehouseCode { get; set; }

        [DisplayName("Name")]
        [MaxLength(50, ErrorMessage = "Name must not exceed to {0} characters.")]
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Code must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "Name  already exists.", PropertyIdFieldName = nameof(WarehouseId), PropertyTableName = "Warehouses")]
        public string WarehouseName { get; set; }
    }
}