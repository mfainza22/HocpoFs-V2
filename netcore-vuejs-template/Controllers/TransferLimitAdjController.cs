using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeighingSystemCore.Models;
using WeighingSystemCore.ViewModels;
using WeighingSystemCoreHelpers.Extensions;
using WeighingSystemCoreHelpers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeighingSystemCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferLimitAdjController : ControllerBase
    {
        private readonly ITransferLimitAdjRepository _tlAdjRepo; 
        private readonly ITransferLimitRepository _tlRepo;

        public TransferLimitAdjController(ITransferLimitAdjRepository repo, ITransferLimitRepository tlrepo)
        {
            _tlAdjRepo = repo;
            _tlRepo = tlrepo;
        }

        [ProducesResponseType(typeof(List<TransferLimitAdj>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("[action]")]
        public IActionResult List(long transferLimitId)
        {
            var model = _tlAdjRepo.List(transferLimitId);
            return Ok(model);
        }

        [ProducesResponseType(typeof(TransferLimitAdj), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult Get(Nullable<Int64> id)
        {
            if (id == null)
            {
                return BadRequest(null);
            }
            else
            {
                var model = _tlAdjRepo.Get(id.Value);
                return Ok(model);
            }
        }

        [ProducesResponseType(typeof(List<TransferLimitAdj>), StatusCodes.Status200OK)]
        [HttpGet("{effectiveDate}/{rawMaterialId}/{shiftId}")]
        public IActionResult Get(DateTime effectiveDate, long rawMaterialId, long shiftId)
        {
            try
            {
                var model = new TransferLimitViewModel
                {
                    EffectiveDate = effectiveDate,
                    RawMaterialId = rawMaterialId,
                    ShiftId = shiftId
                };

                var result = _tlRepo.GetTransferLimitData(model);
                if (result == null)
                {
                    return NotFound("Selected record not found.");
                }

                return Ok(result);

            }
            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransferLimitAdj), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] TransferLimitAdj model)
        {
            model.AdjDate = DateTime.Now;
            ModelState.Remove(nameof(TransferLimitAdj.AdjDate));


            if (ModelState.IsValid)
            {
                _tlAdjRepo.Create(model);
                return Ok(model);
            }
            else
            {
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ResponseObject = ModelState.ToJson() }.ToJsonResult();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TransferLimitAdj), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(long id, [FromBody] TransferLimitAdj model)
        {
            if (ModelState.IsValid)
            {
                model.TransferLimitAdjId = id;
                _tlAdjRepo.Update(model);
                return Ok(model);
            }
            else
            {
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ResponseObject = ModelState.ToJson() }.ToJsonResult();
            }
        }

        [HttpDelete("{name}/{ids}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult BulkDelete(string ids)
        {
            try
            {
                var arrayIds = ids.Split(",");
                if (arrayIds.Length == 0) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "Deleting failed. No record was selected" }.ToJsonResult();


                _tlAdjRepo.Delete(arrayIds);

                return Ok($"Successfully Deleted {arrayIds.Length} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "An error encountred while deleting records." }.ToJsonResult();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Delete(TransferLimitAdj model)
        {
            try
            {
                var existingModel = _tlAdjRepo.Get(model.TransferLimitAdjId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. Selected record does not exist." }.ToJsonResult();

                _tlAdjRepo.Delete(new string[] { existingModel.TransferLimitAdjId.ToString() });

                return Ok($"Successfully Deleted 1 record.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting a record." }.ToJsonResult();
            }
        }

    }
}
