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
    public class WeighingAreaController : ControllerBase
    {
        private readonly IWeighingAreaRepository _repository;
        public WeighingAreaController(IWeighingAreaRepository repo)
        {
            _repository = repo;
        }

        [ProducesResponseType(typeof(List<WeighingArea>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<WeighingArea> Get()
        {
            var model = _repository.List();
            return model;
        }

        [ProducesResponseType(typeof(WeighingArea), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(WeighingArea), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] WeighingArea model)
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
        [ProducesResponseType(typeof(WeighingArea), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(long id, [FromBody] WeighingArea model)
        {
            if (ModelState.IsValid)
            {
                model.WeighingAreaId = id;
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
        public IActionResult Delete(WeighingArea model)
        {
            try
            {
                var existingModel = _repository.Get(model.WeighingAreaId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. {model.AreaDesc} does not exist." }.ToJsonResult();

                _repository.Delete(new string[] { existingModel.WeighingAreaId.ToString() });

                return Ok($"Successfully Deleted {existingModel.AreaDesc} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting {model.AreaDesc}." }.ToJsonResult();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateCode([FromBody] WeighingArea model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByCode(model.AreaCode);
            if (existing == null) return Accepted(true);
            if (existing.WeighingAreaId != model.WeighingAreaId)
            {
                return UnprocessableEntity("Code already exists");
            }
            else
            {
                return Accepted(true);
            }

        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateDesc([FromBody] WeighingArea model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByDesc(model.AreaDesc);
            if (existing == null) return Accepted(true);
            if (existing.WeighingAreaId != model.WeighingAreaId)
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
