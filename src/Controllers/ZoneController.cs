using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using raspberry_mqqt_motion_alarm.Atributes;
using raspberry_mqqt_motion_alarm.Models;
using raspberry_mqqt_motion_alarm.Services;
using raspberry_mqqt_motion_alarm.Services.Interfaces;

namespace raspberry_mqqt_motion_alarm.Controllers
{
    [ApiKeyAuthentication]
    [ApiController]
    [Route("[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly ILogger<ZoneController> _logger;
        private readonly IZoneService zoneService;

        public ZoneController(ILogger<ZoneController> logger, IZoneService zoneService)
        {
            this.zoneService = zoneService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Zone> Get()
        {
            return this.zoneService.FindAll();
        }

        [HttpGet("{id}", Name = "FindOne")]
        public ActionResult<Zone> Get(string id)
        {
            var result = this.zoneService.FindOne(id);
            if (result != default)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Zone> Insert(Zone dto)
        {
            var id = this.zoneService.Insert(dto);
            if (id != default)
                return CreatedAtRoute("FindOne", new { id = id }, dto);
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<Zone> Update(Zone dto)
        {
            var result = this.zoneService.Update(dto);
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
