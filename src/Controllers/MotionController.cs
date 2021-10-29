using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using raspberry_mqqt_motion_alarm.Atributes;
using raspberry_mqqt_motion_alarm.Models;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Controllers
{
    [ApiKeyAuthentication]
    [ApiController]
    [Route("[controller]")]
    public class MotionController : ControllerBase
    {
        private readonly IMotionService motionService;

        public MotionController(IMotionService motionService)
        {
            this.motionService = motionService;
        }

        [HttpGet("{zone}")]
        public IEnumerable<MotionDetector> Get(string zone)
        {
            return this.motionService.FindAll(zone);
        }

        [HttpGet("{zone}/{id}", Name = "FindOneDetector")]
        public ActionResult<Zone> Get(string zone, Guid id)
        {
            var result = this.motionService.FindOne(zone,id);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost("{zone}")]
        public ActionResult<Zone> Insert(string zone, MotionDetector dto)
        {
            var id = this.motionService.Insert(zone,dto);
            if (id != default)
                return CreatedAtRoute("FindOneDetector", new { id = id, zone = zone }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<Zone> Update(Zone dto)
        {
            var result = this.motionService.Update(dto);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<Zone> Delete(string id)
        { 
                return NoContent();
        }
    }
}
