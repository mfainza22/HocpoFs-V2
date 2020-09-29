
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Entities
{
    public class LoginInfo
    {
        [RegularExpression(WeighingSystemCoreHelpers.Attributes.Validations.RegExStrings.INVALID_CHAR, ErrorMessage = "User name must not contain an invalid characters.")]
        [Required(ErrorMessage = "User name is required.")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(WeighingSystemCoreHelpers.Attributes.Validations.RegExStrings.INVALID_CHAR, ErrorMessage = "Password must not contain an invalid characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [DataType(DataType.Url)]
        public string ReturnUrl { get; set; }
    }
}
