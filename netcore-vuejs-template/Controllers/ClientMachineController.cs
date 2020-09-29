
using System;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeighingSystemCore.Models;
using WeighingSystemCoreHelpers.Extensions;
using WeighingSystemCoreHelpers.Models;

namespace WeighingSystemCore.Controllers
{
    [Authorize(Roles = RoleNames.VIEW_CLIENT_MACHINES)]
    public class ClientMachineController : Controller
    {
        private readonly IClientMachineRepository repository;
        public ClientMachineController(IClientMachineRepository _ClientMachineRepository)
        {
            repository = _ClientMachineRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.SubTitle = "-" + "Client Machine Settings";
            return View(new ClientMachine());
        }


        public IActionResult List()
        {
            var model = repository.List();
            Response.StatusCode = (int)StatusCodes.Status200OK;
            return new JsonResult(model);
        }

        [ProducesResponseType(typeof(ClientMachine), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        public IActionResult Get(Nullable<Int64> id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            else
            {
                var model = repository.Get(id.Value);
                return new JsonResult(model);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClientMachine), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = RoleNames.MODIFY_CLIENT_MACHINES)]
        public IActionResult Create([FromBody] ClientMachine model)
        {
            if (ModelState.IsValid)
            {

                repository.Create(model);
                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ClientMachine), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = RoleNames.MODIFY_CLIENT_MACHINES)]
        public IActionResult Update([FromBody] ClientMachine model)
        {
            if (ModelState.IsValid)
            {
                repository.Update(model);
                return Ok(model);
            }
            else
            {
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ResponseObject = ModelState.ToJson() }.ToJsonResult();
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseResult), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = RoleNames.MODIFY_CLIENT_MACHINES)]
        public IActionResult Delete([FromQuery] long[] ids)
        {
            try
            {
                if (ids.Length == 0) return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "Deleting failed. No record was selected" }.ToJsonResult();

                repository.Delete(ids);

                return Ok($"Successfully Deleted {ids.Length} records.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return new ResponseResult(Response) { StatusCode = (int)StatusCodes.Status400BadRequest, ErrorMessage = "An error encountred while deleting records." }.ToJsonResult();
            }
        }
    }
}