using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeighingSystemCore.Models
{
    public class ClientMachine
    {

        [DisplayName("Machine I.D.")]
        public long ClientMachineId { get; set; }


        [Required]
        [DisplayName("Client IP address")]
        [RegularExpression("^(?=.*[^\\.]$)((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.?){4}$",ErrorMessage ="IP address is invalid.")]
        [MaxLength(20,ErrorMessage ="IP Address must not exceed to ")]
        public string ClientIP { get; set; }

        [Required]
        [DisplayName("Weighing Area")]
        public long WeighingAreaId { get; set; }

        [DisplayName("Area Desc")]
        public string AreaDesc { get; set;}

        public string ReceiptNumPrefix { get; set; }

    }
}
