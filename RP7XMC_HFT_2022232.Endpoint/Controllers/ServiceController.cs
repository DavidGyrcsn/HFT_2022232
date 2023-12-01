using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RP7XMC_HFT_2022232.Endpoint.Services;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RP7XMC_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        IServiceLogic logic;
        IHubContext<SignalRHub> hub;

        public ServiceController(IServiceLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<BrandController>
        [HttpGet]
        public IEnumerable<Service> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<BrandController>/5
        [HttpGet("{id}")]
        public Service Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<BrandController>
        [HttpPost]
        public void Create([FromBody] Service value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("ServiceCreated", value);
        }

        // PUT api/<BrandController>/5
        [HttpPut]
        public void Update(int id, [FromBody] Service value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("ServiceUpdated", value);
        }

        // DELETE api/<BrandController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var serviceToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("ServiceDeleted", serviceToDelete);
        }
    }
}
