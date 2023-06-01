using Microsoft.AspNetCore.Mvc;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RP7XMC_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarLogic logic;

        public CarController(ICarLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<CarController>
        [HttpGet]
        public IEnumerable<Car> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<CarController>/5
        [HttpGet("{id}")]
        public Car Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<CarController>
        [HttpPost]
        public void Create([FromBody] Car value)
        {
            this.logic.Create(value);
        }

        // PUT api/<CarController>/5
        [HttpPut]//("{id}")]
        public void Update(int id, [FromBody] Car value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<CarController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        { 
            this.logic.Delete(id);
        }
    }
}
