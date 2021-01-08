using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reti.PortalePercorsi.BL.DTO;
using Reti.PortalePercorsi.BL.Manager;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reti.PortalePercorsi.SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly RoomsManager roomsManager;
        private readonly ILogger _logger;
        public RoomsController(IUnitOfWork iunitofwork, ILogger<LessonsController> logger)
        {
            roomsManager = new RoomsManager(iunitofwork);
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Request Rooms/GetAll");
                return Ok(roomsManager.GetAll());

            }
            catch (Exception ex)
            {
                roomsManager.Dispose();
                _logger.LogError("Error during Rooms/GetAll", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            try
            {
                _logger.LogInformation("Request Rooms/GetById");
                return Ok(roomsManager.GetById(Id));

            }
            catch (Exception ex)
            {
                roomsManager.Dispose();
                _logger.LogError("Error during Rooms/GetById", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] DTORoom dtoRoom)
        {
            try
            {
                _logger.LogInformation("Request Rooms/Add");
                return Ok(roomsManager.Add(dtoRoom));

            }
            catch (Exception ex)
            {
                roomsManager.Dispose();
                _logger.LogError("Error during Rooms/Add", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{Id}")]
        public IActionResult Remove(int Id)
        {
            try
            {
                _logger.LogInformation("Request Rooms/CanDelete");
                roomsManager.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                roomsManager.Dispose();
                _logger.LogError("Error during Rooms/CanDelete", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("CanDelete/{Id}")]
        public IActionResult CanDelete(int Id)
        {
            try
            {
                _logger.LogInformation("Request Rooms/Remove");
                bool deleted = roomsManager.CanDelete(Id);
                return Ok(deleted);
            }
            catch (Exception ex)
            {
                roomsManager.Dispose();
                _logger.LogError("Error during Rooms/Remove", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}