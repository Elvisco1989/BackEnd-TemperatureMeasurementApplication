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
                //Hent målinger fra repository
                IEnumerable<TemperatureMeasurement> measurements = _tempRepository.GetTempList(date, orderBy);
                if (measurements.Count() == 0)
                {
                    //Hvis der ikke er nogen målinger, returner "No Content" statuskode
                    return NoContent();
                }

                //Returnerer målinger
                return Ok(measurements);
            }
            catch (Exception ex)
            {
                //Hvis der opstår en fejl, returner "Internal Server Error" statuskode med fejlbesked
                return StatusCode(StatusCodes.Status500InternalServerError,"An error occured while processing your request"+ex.Message);
            }

        }

        // GET api/<TempController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TemperatureMeasurement> Get(int id)
        {
            //Hent måling ved givet id fra repository
            TemperatureMeasurement? measurement = _tempRepository.GetID(id);
            if (measurement == null)
            {
                //Hvis målingen ikke findes, returneres en "Not Found" statuskode
                return NotFound();
            }
            //Returnerer måling
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
                //Valider målings indendørs temperatur, udendørs temperatur og dato
                measurement.ValidateInDoorTemperature();
                measurement.ValidateOutDoorTemperature();
                measurement.ValidateDateTime();
            }
            catch (System.Exception ex)
            {
                //Hvis der opstår en fejl, returneres en "Bad Request" med fejlbesked
                return BadRequest(ex.Message);
            }

            //Tilføj måling til repository
            _tempRepository.Add(measurement);

            //Returnerer en "Created" statuskode med den nye measurement
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
                //Valider målings indendørs temperatur, udendørs temperatur og dato
                measurement.ValidateInDoorTemperature();
                measurement.ValidateOutDoorTemperature();
                measurement.ValidateDateTime();
            }
            catch (System.Exception ex)
            {
                //Hvis der opstår en fejl, returneres en "Bad Request" med fejlbesked
                return BadRequest(ex.Message);
            }

            //Opdater måling ved givet id fra repository
            TemperatureMeasurement? updatedMeasurement = _tempRepository.Update(id, measurement);
            if (updatedMeasurement == null)
            {
                //Hvis målingen ikke findes, returneres en 404
                return NotFound();
            }

            //returner den opdaterede measurement
            return Ok(updatedMeasurement);

        }

        // DELETE api/<TempController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<TemperatureMeasurement?> Delete(int id) 
        {
            //Slet måling ved givet id fra repository
            TemperatureMeasurement? measurement = _tempRepository.Delete(id);
            if (measurement == null)
            {
                //Hvis målingen ikke findes, returneres en 404
                return NotFound();
            }

            //Returner den slettede measurement
            return Ok(measurement);

        }
    }
}
