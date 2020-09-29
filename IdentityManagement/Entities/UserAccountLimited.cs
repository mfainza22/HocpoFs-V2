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
    public class UserAccountLimited
    {

        [HiddenInput]
        [Display(Name = "User Account I.D.")]
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }


        [Display(Name = "Full Name")]
        public string FullName { get; set; }


    }
}