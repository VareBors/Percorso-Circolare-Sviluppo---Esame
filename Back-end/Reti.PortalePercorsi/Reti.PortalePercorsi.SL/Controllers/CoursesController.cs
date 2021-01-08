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
    public class CoursesController : ControllerBase
    {
        private readonly CourseManager courseManager;
        private readonly ILogger _logger;
        public CoursesController(IUnitOfWork iunitofwork, ILogger<LessonsController> logger)
        {
            courseManager = new CourseManager(iunitofwork);
            _logger = logger;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Request Courses/GetAll");
                return Ok(courseManager.GetAll());

            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Courses/GetAll", ex.Message);
                courseManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            try
            {
                _logger.LogInformation("Request Courses/GetById");
                return Ok(courseManager.GetById(Id));

            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Courses/GetById", ex.Message);
                courseManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]DTOCourse dtoCourse)
        {
            try
            {
                _logger.LogInformation("Request Courses/Add");
                return Ok(courseManager.Add(dtoCourse));
            }
            catch(Exception ex)
            {
                _logger.LogError("Error during Courses/Add", ex.Message);
                courseManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] DTOCourse dtoCourse)
        {
            try
            {
                _logger.LogInformation("Request Courses/Add");
                return Ok(courseManager.Edit(dtoCourse));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Courses/Add", ex.Message);
                courseManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public IActionResult Remove(int Id)
        {
            try
            {
                _logger.LogInformation("Request Courses/Remove");
                courseManager.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Courses/Remove", ex.Message);
                courseManager.Dispose();
                return StatusCode(500, ex.Message);
            }
        }
    }
}
