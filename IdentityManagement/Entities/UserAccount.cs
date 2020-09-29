using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeighingSystemCoreHelpers.Attributes.Validations;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using IdentityManagement.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Entities
{
    public class UserAccount
    {

        [HiddenInput]
        [Display(Name = "User Account I.D.")]
        public string Id { get; set; }

        [Display(Name = "User Name")]
        [MaxLength(100)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User name is required")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "User name must not contain an invalid characters.")]
        [ExistingValidator(ErrorMessage = "User name already exists.", PropertyIdFieldName = nameof(Id), PropertyTableName = "UserAccounts")]
        public string UserName { get; set; }

        public string UserPwdHash { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Password must not contain an invalid characters.")]
        [Compare("UserPwdStringConfirm", ErrorMessage = "Confirm password doesn't match.")]
        public string UserPwdString { get; set; }


        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Confirm Password must not contain an invalid characters.")]
        public string UserPwdStringConfirm { get; set; }

        public string UserPwdHashConfirm { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Last name must not exceed to 50 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Last name must not contain an invalid characters.")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(50, ErrorMessage = "Midel name must not exceed to 50 characters")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Middle name must not contain an invalid characters.")]
        public string MiddleName { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "First name must not exceed to 50 characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "First name must not contain an invalid characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Suffix")]
        [MaxLength(5, ErrorMessage = "Position must not exceed to 5 characters")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Suffix name must not contain an invalid characters.")]
        public string NameSuffix { get; set; }

        [Display(Name = "Department")]
        [MaxLength(50, ErrorMessage = "Department must not exceed to 50 characters")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Department must not contain an invalid characters.")]
        public string Department { get; set; }


        [Display(Name = "Position")]
        [MaxLength(50, ErrorMessage = "Position must not exceed to 50 characters")]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Position must not contain an invalid characters.")]
        public string Position { get; set; }

        [Display(Name = "Email Address")]
        [MaxLength(255)]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Email Address must not contain an invalid characters.")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        [MaxLength(255)]
        [RegularExpression(RegExStrings.INVALID_CHAR, ErrorMessage = "Phone number must not contain an invalid characters.")]
        public string PhoneNumber { get; set; }

        public short AccessFailedCount { get; set; }

        [Display(Name = "Date Created ")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Registered By")]
        public string RegisteredBy { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Modified By")]
        public Nullable<DateTime> TimeStamp { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public EnumUserStatus Status { get; set; }

        public string StatusString
        {
            get; set;
        }

    }
}