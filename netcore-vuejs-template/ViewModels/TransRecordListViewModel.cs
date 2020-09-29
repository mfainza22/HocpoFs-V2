using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WeighingSystemCore.Models;
using WeighingSystemCoreHelpers.Attributes.Validations;
using WeighingSystemCoreHelpers.Enums;

namespace WeighingSystemCore.ViewModels
{
    public class TransRecordListViewModel : TransRecord
    {
        public string RawMaterialDesc { get; set; }

        public string BinLocDesc { get; set; }

        public string LocationName { get; set; }

        public string WarehouseName { get; set; }

        public string PackagingTypeDesc { get; set; }

        public string ShiftDesc { get; set; }

        public string WeigherInName { get; set; }

        public string WeigherOutName { get; set; }

        public string AreaDesc { get; set; }
    }
}