using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Constants;
using WeighingSystemCoreHelpers.Enums;
using WeighingSystemCoreHelpers.Extensions;
using WeighingSystemCoreHelpers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeighingSystemCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransRecordController : ControllerBase
    {
        private readonly ITransRecordRepository transRepository;
        //private readonly UserManager<UserAccount> userManager;
        private readonly IClientMachineRepository clientMachineRepository;
        private readonly IGeneralSettingsRepository genSettingsRepository;
        private readonly ITransferLimitRepository transferLimitRepository;
        private readonly IShiftRepository shiftRepository;

        public TransRecordController(ITransRecordRepository transRecordRepository,
          //UserManager<UserAccount> _userManager,
          IClientMachineRepository _clientMachineRepository,
          IGeneralSettingsRepository _genSettingsRepository,
          ITransferLimitRepository transferLimitRepository,
          IShiftRepository shiftRepository)
        {
            this.transRepository = transRecordRepository;
            //userManager = _userManager;
            clientMachineRepository = _clientMachineRepository;
            genSettingsRepository = _genSettingsRepository;
            this.transferLimitRepository = transferLimitRepository;
            this.shiftRepository = shiftRepository;

        }

        [ProducesResponseType(typeof(List<TransRecordViewModel>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get([FromQuery] TransRecordFilter filter)
        {
            if (filter == null)
            {
                filter = new TransRecordFilter()
                {
                    FilterInboundDate = true,
                    DTInboundFrom = DateTime.Now,
                    DTInboundTo = DateTime.Now,
                    TransactionStatus = Enums.TransactionStatus.PENDING.ToString()
                };
            }


            if (filter == null)
            {
                filter = new TransRecordFilter
                {
                    FilterInboundDate = false,
                    TransactionStatus = Enums.TransactionStatus.PENDING.ToString(),
                    DTInboundFrom = DateTime.Now,
                    DTInboundTo = DateTime.Now
                };
            }

            if (filter.TransactionStatus == Enums.TransactionStatus.PENDING.ToString())
            {
                filter.FilterInboundDate = false;
                filter.FilterOutboundDate = false;
                filter.DTInboundFrom = filter.DTOutboundFrom;
                filter.DTInboundTo = filter.DTOutboundTo;
            }
            else
            {
                filter.FilterInboundDate = false;
                filter.FilterOutboundDate = true;
                filter.DTOutboundFrom = filter.DTInboundFrom;
                filter.DTOutboundTo = filter.DTInboundTo;
            }

            var selectFields = new System.Text.StringBuilder();
            selectFields.Append("TransactionId" + (char)44);
            selectFields.Append("ReceiptNum" + (char)44);
            selectFields.Append("DTInbound" + (char)44);
            selectFields.Append("DTOutbound" + (char)44);
            selectFields.Append("RawMaterialDesc" + (char)44); ;
            selectFields.Append("BinLocDesc" + (char)44);
            selectFields.Append("PalletNum" + (char)44);
            selectFields.Append("BinNum" + (char)44);
            selectFields.Append("GrossWt" + (char)44);
            selectFields.Append("TareWt" + (char)44);
            selectFields.Append("NetWt" + (char)44);
            selectFields.Append("Quantity" + (char)44);
            selectFields.Append("ControlNum" + (char)44);
            selectFields.Append("WeigherInName");
            filter.SelectField = selectFields.ToString();

            var orderByClause = filter.TransactionStatus == Enums.TransactionStatus.PENDING.ToString() ? "ORDER BY DTInbound DESC" : "ORDER BY DTOutbound DESC";
            //filter.OrderByClause = orderByClause;
            
            var model = transRepository.List(filter.ToString());
            var result = model.OrderBy(a => a.DTInbound).Select(a => new
            {
                a.TransactionId,
                DT = filter.TransactionStatus == Enums.TransactionStatus.PENDING.ToString() ? a.DTInbound.Value.ToString("MMM-dd-yyy hh:mm tt") : a.DTOutbound.Value.ToString("MMM-dd-yyy hh:mm tt"),
                a.ReceiptNum,
                a.ControlNum,
                a.PalletNum,
                a.GrossWt,
                a.TareWt,
                a.NetWt,
                a.RawMaterialDesc,
                a.Quantity,
                a.PackagingTypeDesc,
                a.BinNum,
                a.BinLocDesc,
                a.LocationName,
                a.WeigherInName,
                a.ShiftId,
                a.ShiftDesc
            }).ToList();

            return Ok(result);
        }

        [ProducesResponseType(typeof(TransRecord), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpGet("[action]/{id}")]
        public IActionResult Pending(Nullable<Int64> id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var model = transRepository.Get(id.Value, Enums.TransactionStatus.PENDING);
                StoreSettings(ref model);
                return Ok(model);
            }
        }

        [ProducesResponseType(typeof(TransRecord), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpGet("[action]/{id}")]
        public IActionResult Completed(Nullable<Int64> id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var model = transRepository.Get(id.Value, Enums.TransactionStatus.COMPLETED);
                return Ok(model);
            }
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult WeighIn([FromBody] TransRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsOffline)
                {
                    model.DTInbound = model.DTOfflineDate;
                    model.WtInbound = model.OfflineWt;
                } else
                {
                    model.DTInbound = DateTime.Now;
                    model.WtInbound = model.WtInbound;
                }
                var currentShift = shiftRepository.GetCurrentShift(model.DTInbound.Value);
                model.ShiftId = currentShift.ShiftId;
                model.ShiftDate = currentShift.ShiftDate;

                var limit = transferLimitRepository.CheckLimit(new TransferLimitViewModel()
                {
                    EffectiveDate = model.DTInbound,
                    RawMaterialId = model.RawMaterialId,
                    ShiftId = model.ShiftId
                });

                if (limit == null || limit.ComputedLimitKg == 0)
                {
                    ModelState.AddModelError(nameof(TransRecord.RawMaterialId), "No Limit was set. Transaction cannot continue");
                    return BadRequest(ModelState.ToJson());
                }


                model.TicketType = Enums.TicketType.IN.ToString();

                //var currentUserId = userManager.GetUserId(HttpContext.User);
                model.WeigherInId = "cea78691-84a0-411b-b29d-89b17d8e4c69";
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                var clientMachine = clientMachineRepository.CheckClientMachine(remoteIpAddress.ToString());
                //model.ReceiptNumPrefix = clientMachine.ReceiptNumPrefix;
                model.WeighingAreaId = clientMachine.WeighingAreaId;
                model.TransactionProcess = Enums.TransactionProcess.WEIGH_IN.ToString();
                transRepository.WeighIn(model);

                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState.ToJson());
            }
        }

        [HttpPut("[action]")]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult WeighOut([FromBody] TransRecordViewModel model)
        {
            if (model.WtOutbound == 0) ModelState.AddModelError(nameof(model.WtOutbound), "Invalid Weight.");

            if (ModelState.IsValid)
            {
                transRepository.WeighOut(model);
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState.ToJson());
            }
        }

        [HttpPut("[action]/{id}")]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Update(long id, [FromBody] TransRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.LocationId = id;
                transRepository.Update(model,model.DTOutbound == null ? Enums.TransactionStatus.PENDING : Enums.TransactionStatus.COMPLETED);
                return Ok(model);
            }
            else
            {
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ResponseObject = ModelState.ToJson() }.ToJsonResult();
            }
        }


        [HttpDelete("[action]/{ids}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Pending(string ids)
        {
            try
            {
                var arrayIds = ids.Split(",");
                if (arrayIds.Length == 0) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "Deleting failed. No record was selected" }.ToJsonResult();

                transRepository.Delete(arrayIds,Enums.TransactionStatus.PENDING);

                return Ok($"Successfully Deleted {arrayIds.Length} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "An error encountred while deleting records." }.ToJsonResult();
            }
        }

        [HttpDelete("[action]/{ids}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Completed(string ids)
        {
            try
            {
                var arrayIds = ids.Split(",");
                if (arrayIds.Length == 0) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "Deleting failed. No record was selected" }.ToJsonResult();

                transRepository.Delete(arrayIds, Enums.TransactionStatus.PENDING);

                return Ok($"Successfully Deleted {arrayIds.Length} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "An error encountred while deleting records." }.ToJsonResult();
            }
        }

        private void StoreSettings(ref TransRecordViewModel model)
        {
            if (model != null)
            {
                var genSet = genSettingsRepository.Get();
                model.TolActualWt = genSet.Tolerance;
            }
        }
    }
}
