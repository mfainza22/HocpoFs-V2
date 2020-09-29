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
    public class PackagingTypeController : ControllerBase
    {
        private readonly IPackagingTypeRepository _repository;
        public PackagingTypeController(IPackagingTypeRepository binLocRepository)
        {
            _repository = binLocRepository;
        }

        [ProducesResponseType(typeof(List<PackagingType>), StatusCodes.Status200OK)]
        [HttpGet]
        public IEnumerable<PackagingType> Get()
        {
            var model = _repository.List();
            return model;
        }

        [ProducesResponseType(typeof(PackagingType), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(PackagingType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] PackagingType model)
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
        [ProducesResponseType(typeof(PackagingType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Put(int id, [FromBody] PackagingType model)
        {
            if (ModelState.IsValid)
            {
                model.PackagingTypeId = id;
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
        public IActionResult Delete(PackagingType model)
        {
            try
            {
                var existingModel = _repository.Get(model.PackagingTypeId);
                if (existingModel == null) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"Deleting failed. {model.PackagingTypeDesc} does not exist." }.ToJsonResult();

                _repository.Delete(new string[] { existingModel.PackagingTypeId.ToString() });

                return Ok($"Successfully Deleted {existingModel.PackagingTypeDesc} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = $"An error encountred while deleting {model.PackagingTypeDesc}." }.ToJsonResult();
            }
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult ValidateDesc ([FromBody] PackagingType model)
        {
            //var serializerSettings = new
            //{
            //    ContentType = "application/json",
            //    StatusCode = StatusCodes.Status202Accepted,
            //};
            // var result = new JsonResult("true", serializerSettings);
            if (model == null) return NotFound();
            var existing = _repository.Get(model.PackagingTypeDesc);
            if (existing == null) return Accepted(true);
            if (existing.PackagingTypeId != model.PackagingTypeId)
            {
                return UnprocessableEntity("Description already exists");
            } else
            {
                return Accepted(true);
            }
        }

    }
}
