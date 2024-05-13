using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeatWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private TempRepo _tempRepository;

        public TempController(TempRepo tempRepository)
        {
            _tempRepository = tempRepository;
        }

        // GET: api/<TempController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<TemperatureMeasurement>> Get([FromQuery] DateTime? date=null, [FromQuery] string? orderBy=null ) 
        {
            IEnumerable<TemperatureMeasurement>measurements = _tempRepository.GetTempList(date, orderBy);
            if (measurements.Count() == 0)
            {
                return NoContent();
            }
            return Ok(measurements);

        }

        // GET api/<TempController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TempController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TemperatureMeasurement> Post([FromBody] TemperatureMeasurement measurement)
        {
            try
            {
                measurement.ValidateInDoorTemperature();
                measurement.ValidateOutDoorTemperature();
                measurement.ValidateDateTime();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            _tempRepository.Add(measurement);
            return CreatedAtAction(nameof(Get), new { id = measurement.Id }, measurement);

        }

        // PUT api/<TempController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TempController>/5
        [HttpDelete("{id}")]
        
        public void Delete(int id)
        {
        }
    }
}
