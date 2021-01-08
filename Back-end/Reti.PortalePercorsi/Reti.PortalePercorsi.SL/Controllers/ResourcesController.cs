using Microsoft.AspNetCore.Mvc;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using Reti.PortalePercorsi.BL.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reti.PortalePercorsi.BL.DTO;
using Microsoft.Extensions.Logging;

namespace Reti.PortalePercorsi.SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ResourcesManager resourceManager;
        private readonly ILogger _logger;

        public ResourcesController(IUnitOfWork iunitofwork, ILogger<LessonsController> logger)
        {
            resourceManager = new ResourcesManager(iunitofwork);
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Request Lessons/GetAll");
                return Ok(resourceManager.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Resource/GetAll", ex.Message);
                resourceManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            try
            {
                _logger.LogInformation("Request Lessons/GetAll", Id);

                return Ok(resourceManager.GetById(Id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Resource/GetById/"+Id, ex.Message);
                resourceManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] DTOResource dtoResource)
        {
            try
            {
                int newResourceId = resourceManager.Add(dtoResource);
                _logger.LogInformation("Added new Resource", newResourceId);
                return Ok(newResourceId);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error during Resource/Add", ex.Message);
                resourceManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }
    }
}
