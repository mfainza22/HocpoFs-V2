
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
    public class RawMaterialController : ControllerBase
    {
        private readonly IRawMaterialRepository _repository;
        public RawMaterialController(IRawMaterialRepository repository)
        {
            _repository = repository;
        }

        [ProducesResponseType(typeof(List<RawMaterial>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<RawMaterial> Get()
        {
            var model = _repository.List().OrderBy(a=>a.RawMaterialDesc).ToList();
            return model;
        }

        [ProducesResponseType(typeof(RawMaterial), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public IActionResult Get(Nullable<Int64> id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                var model = _repository.Get(id.Value);
                return Ok(model);
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(RawMaterial), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] RawMaterial model)
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
        [ProducesResponseType(typeof(RawMaterial), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] RawMaterial model)
        {
            if (ModelState.IsValid)
            {
                model.RawMaterialId = id;
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
        public IActionResult Delete(RawMaterial model)
        {
            try
            {
                var existingModel = _repository.Get(model.RawMaterialId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. {model.RawMaterialDesc} does not exist." }.ToJsonResult();

                _repository.Delete(new string[] { existingModel.RawMaterialId.ToString() });

                return Ok($"Successfully Deleted {existingModel.RawMaterialDesc} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting {model.RawMaterialDesc}." }.ToJsonResult();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateCode([FromBody] RawMaterial model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByCode(model.RawMaterialCode);
            if (existing == null) return Accepted(true);
            if (existing.RawMaterialId != model.RawMaterialId)
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
        public IActionResult ValidateDesc([FromBody] RawMaterial model)
        {
            if (model == null) return NotFound();
            var existing = _repository.GetByDesc(model.RawMaterialDesc);
            if (existing == null) return Accepted(true);
            if (existing.RawMaterialId != model.RawMaterialId)
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




