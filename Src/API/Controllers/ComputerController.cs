using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Business;
using API.Business.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace API.Controllers
{

    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ComputerController : Controller
    {
        private readonly IBussinessLayer _BussinessLayer;

        //TODO : Implement a NLOG on this controller.

        public ComputerController(IBussinessLayer computerBussinessLayer)
        {
            _BussinessLayer = computerBussinessLayer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_BussinessLayer.FindAll());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var computer = _BussinessLayer.FindById(id);

                if (computer == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(computer);
                }
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Capture computer)
        {
            try
            {
                if (computer == null)
                {
                    return BadRequest();
                }
                else
                {
                    return new ObjectResult(_BussinessLayer.Create(computer));
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
