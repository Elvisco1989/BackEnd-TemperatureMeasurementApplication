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
        public ActionResult<IEnumerable<TemperatureMeasurement>> Get([FromQuery] DateTime? date=null, [FromQuery] string? orderBy=null ) 
        {
            IEnumerable<TemperatureMeasurement>measurements = _tempRepository.GetTempList(date, orderBy);
           
        }

        // GET api/<TempController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TempController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
