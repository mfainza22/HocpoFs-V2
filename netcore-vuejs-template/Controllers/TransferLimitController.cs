
using System;
using System.Linq;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeighingSystemCore.Constants;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Constants;
using WeighingSystemCoreHelpers.Extensions;
using WeighingSystemCoreHelpers.Models;

namespace WeighingSystemCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferLimitController : ControllerBase
    {
        private readonly ITransferLimitRepository _tlRepository;
        private readonly IShiftRepository _shiftRepository;

        public TransferLimitController(ITransferLimitRepository tlRepository,
            IShiftRepository shiftRepository)
        {
            _tlRepository = tlRepository;
            _shiftRepository = shiftRepository;

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult list(DateTime? dt)
        {
            dt = dt ?? DateTime.Now;
            var model = _tlRepository.ListByEffectiveDate2(dt);
            if (model.Count() == 0)
            {
               _tlRepository.AutoGenTransferLimit(dt.Value, "0001");
                model = _tlRepository.ListByEffectiveDate2(dt);
            }

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TransferLimitViewModel model)
        {
            System.Diagnostics.Debug.Write(ModelState.ToJson());

            if (ModelState.IsValid)
            {
                var existing = _tlRepository.Get(model);
                if (existing != null)
                {
                    ModelState.AddModelError(nameof(TransferLimit.ShiftId), "Standard limit already exists.");
                    return BadRequest(ModelState);
                }
                _tlRepository.Create(model);
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] EditTransferLimitViewModel model)
        {
            var props = new TransferLimitViewModel()
            {
                EffectiveDate = model.EffectiveDate,
                RawMaterialId = model.RawMaterialId,
                ShiftId = model.ShiftId
            };

            var tlimitVM = _tlRepository.Get(props);
            if (tlimitVM == null)
            {
                return NotFound("Selected record not found.");
            } else
            {
                tlimitVM.ComputedLimitKg = model.ComputedLimitKg;
            }

            if (ModelState.IsValid)
            {
                _tlRepository.Update(tlimitVM);
                return Ok(tlimitVM);
            }
            else
            {
                return BadRequest(ModelState.ToJson());
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult CheckLimit([FromBody] [Bind(nameof(TransRecordViewModel.TransactionProcess),
            nameof(TransRecordViewModel.IsOffline),
            nameof(TransRecordViewModel.DTInbound),
            nameof(TransRecordViewModel.DTOutbound),
            nameof(TransRecordViewModel.WtInbound),
            nameof(TransRecordViewModel.NetWt),
            nameof(TransRecordViewModel.ActualNetWt),
            nameof(TransRecord.NetWt),
            nameof(TransRecord.RawMaterialId),
            nameof(TransRecord.BinNum))] TransRecordViewModel model)
        {

            var isWeighIn = model.TransactionProcess == WeighingSystemCoreHelpers.Enums.Enums.TransactionProcess.WEIGH_IN.ToString();
            var isWeighOut = model.TransactionProcess == WeighingSystemCoreHelpers.Enums.Enums.TransactionProcess.WEIGH_OUT.ToString();
            var isUpdateOut = model.TransactionProcess == WeighingSystemCoreHelpers.Enums.Enums.TransactionProcess.UPDATE_OUT.ToString();

            if (isWeighIn) model.DTInbound = model.IsOffline ? model.DTOfflineDate : DateTime.Now;
            else model.DTOutbound = model.IsOffline ? model.DTOfflineDate : DateTime.Now;

            DateTime? selectedDt = isWeighIn ? model.DTInbound : isWeighOut ? model.DTOutbound : isUpdateOut ? model.DTOutbound : model.DTInbound;
            var shiftId = _shiftRepository.GetCurrentShift(selectedDt ?? DateTime.Now).ShiftId;

            var transferLimitVM = new TransferLimitViewModel()
            {
                EffectiveDate = selectedDt,
                ShiftId = shiftId,
                RawMaterialId = model.RawMaterialId
            };

            var result = _tlRepository.CheckLimit(transferLimitVM);

            return Ok(result);
        }

    }
}