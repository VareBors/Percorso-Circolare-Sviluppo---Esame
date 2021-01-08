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
    public class LessonsController : ControllerBase
    {
        private readonly LessonsManager lessonsMananger;
        private readonly ILogger _logger;
        public LessonsController(IUnitOfWork iunitofwork, ILogger<LessonsController> logger)
        {
            lessonsMananger = new LessonsManager(iunitofwork);
            _logger = logger;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                _logger.LogInformation("Request Lessons/GetAll");
                return Ok(lessonsMananger.GetAll());

            }
            catch (Exception ex)
            {
                lessonsMananger.Dispose();
                _logger.LogError("Error during Lesssons/GetAll", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            try
            {
                _logger.LogInformation("Request Lessons/GetById");
                return Ok(lessonsMananger.GetById(Id));
            }
            catch (Exception ex)
            {
                lessonsMananger.Dispose();
                _logger.LogError("Error during Lesssons/GetById", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] DTOLesson dtoLesson)
        {
            try
            {
                _logger.LogInformation("Request Lessons/Add");
                string ErrorText = "";
                int lessonID = lessonsMananger.Add(dtoLesson,out ErrorText);
                if(ErrorText == "") 
                { 
                    return Ok(lessonID);
                }
                else
                {
                    return BadRequest(ErrorText);
                }

            }
            catch (Exception ex)
            {
                lessonsMananger.Dispose();
                _logger.LogError("Error during Lesssons/Add", ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Edit([FromBody] DTOLesson dtoLesson)
        {
            try
            {
                string ErrorText = "";
                _logger.LogInformation("Request Lessons/Edit");
                int EditReturn = lessonsMananger.Update(dtoLesson, out ErrorText);
                if(ErrorText == string.Empty)
                {
                    return Ok(EditReturn);
                }
                else
                {
                    return BadRequest(ErrorText);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError("Error during Lesssons/GetById", ex.Message);
                lessonsMananger.Dispose();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{Id}")]
        public IActionResult Remove(int Id)
        {
            try
            {
                _logger.LogInformation("Request Lesssons/Remove");
                lessonsMananger.Remove(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during Lesssons/Remove", ex.Message);
                lessonsMananger.Dispose();
                return StatusCode(500, ex.Message);
            }
        }
    }
}