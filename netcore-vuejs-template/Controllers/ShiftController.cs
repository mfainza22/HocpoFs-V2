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
    public class ShiftController : ControllerBase
    {
        private readonly IShiftRepository _repository;
        public ShiftController(IShiftRepository binLocRepository)
        {
            _repository = binLocRepository;
        }

        [ProducesResponseType(typeof(List<Shift>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<Shift> Get()
        {
            var model = _repository.List();
            return model;
        }

        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] Shift model)
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
        [ProducesResponseType(typeof(Shift), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(long id, [FromBody] Shift model)
        {
            if (ModelState.IsValid)
            {
                model.ShiftId = id;
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
        public IActionResult Delete(Shift model)
        {
            try
            {
                var existingModel = _repository.Get(model.ShiftId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. {model.ShiftDesc} does not exist." }.ToJsonResult();

                _repository.Delete(new string[] { existingModel.ShiftId.ToString() });

                return Ok($"Successfully Deleted {existingModel.ShiftDesc} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting {model.ShiftDesc}." }.ToJsonResult();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateCode([FromBody] Shift model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByCode(model.ShiftCode);
            if (existing == null) return Accepted(true);
            if (existing.ShiftId != model.ShiftId)
            {
                return UnprocessableEntity("Description already exists");
            }
            else
            {
                return Accepted(true);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateDesc([FromBody] Shift model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByDesc(model.ShiftDesc);
            if (existing == null) return Accepted(true);
            if (existing.ShiftId != model.ShiftId)
            {
                return UnprocessableEntity("Description already exists");
            }
            else
            {
                return Accepted(true);
            }
        }

    }
}
