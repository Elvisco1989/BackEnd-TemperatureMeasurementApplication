using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HeatWave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private TempRepoDB _tempRepository;

        public TempController(TempRepoDB tempRepository)
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
            try
            {


                IEnumerable<TemperatureMeasurement> measurements = _tempRepository.GetTempList(date, orderBy);
                if (measurements.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(measurements);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occured while processing your request"+ex.Message);
            }

        }

        // GET api/<TempController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TemperatureMeasurement> Get(int id)
        {
            TemperatureMeasurement? measurement = _tempRepository.GetID(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return Ok(measurement);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<TemperatureMeasurement> Put(int id, [FromBody] TemperatureMeasurement measurement)
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
            TemperatureMeasurement? updatedMeasurement = _tempRepository.Update(id, measurement);
            if (updatedMeasurement == null)
            {
                return NotFound();
            }
            return Ok(updatedMeasurement);

        }

        // DELETE api/<TempController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<TemperatureMeasurement?> Delete(int id) 
        {
            TemperatureMeasurement? measurement = _tempRepository.Remove(id);
            if (measurement == null)
            {
                return NotFound();
            }
            return Ok(measurement);

        }
    }
}
