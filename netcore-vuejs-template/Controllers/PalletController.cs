
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeighingSystemCore.Models;
using WeighingSystemCoreHelpers.Extensions;
using WeighingSystemCoreHelpers.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeighingSystemCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletController : ControllerBase
    {
        private readonly IPalletRepository _repository;
        public PalletController(IPalletRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(List<Pallet>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Pallet> Get()
        {
            var model = _repository.List();
            return model;
        }

        [ProducesResponseType(typeof(Pallet), StatusCodes.Status200OK)]
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
                var model = _repository.Get(id.Value);
                return Ok(model);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Pallet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Pallet model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(model);
                return Ok(model);
            }
            else
            {
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ResponseObject = ModelState.ToJson() }.ToJsonResult();
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Pallet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] Pallet model)
        {
            if (ModelState.IsValid)
            {
                model.PalletId = id;
                _repository.Update(model);
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

                _repository.Delete(arrayIds);

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
        public IActionResult Delete(Pallet model)
        {
            try
            {
                var existingModel = _repository.Get(model.PalletId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. {model.PalletNum} does not exist." }.ToJsonResult();

                _repository.Delete(new string[] { existingModel.PalletId.ToString() });

                return Ok($"Successfully Deleted {existingModel.PalletNum} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting {model.PalletNum}." }.ToJsonResult();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateDesc(Pallet model)
        {
            if (model == null) return NotFound();
            var existing = _repository.Get(model.PalletNum);
            if (existing == null) return Accepted(true);
            if (existing.PalletId != model.PalletId)
            {
                return UnprocessableEntity("Pallet Number already exists");
            }
            else
            {
                return Accepted(true);
            }
        }

        public IActionResult GetUpdatedWt(string palletNum)
        {
            dynamic result = new System.Dynamic.ExpandoObject();
            result.UpdatedWt = 0;
            try
            {
                var updatedWt = _repository.GetUpdatedWtByName(palletNum);
                result.UpdatedWt = updatedWt;
                return Ok(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

    }
}



    
