using Microsoft.AspNetCore.Mvc;
using RP7XMC_HFT_2022232.Logic;
using RP7XMC_HFT_2022232.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RP7XMC_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IBrandLogic brandLogic;
        ICarLogic carLogic;
        IServiceLogic serviceLogic;

        public ValuesController(IBrandLogic brandLogic, ICarLogic carLogic, IServiceLogic serviceLogic)
        {
            this.brandLogic = brandLogic;
            this.carLogic = carLogic;
            this.serviceLogic = serviceLogic;
        }

        [HttpGet]
        public IEnumerable<string> HighestCost()
        {
            return brandLogic.HighestCost();
        }
        [HttpGet]
        public IEnumerable<string> LowestCost()
        {
            return brandLogic.LowestCost();
        }
        [HttpGet]
        public IEnumerable<string> AverageCostForAllBrands()
        {
            return brandLogic.AverageCostForAllBrands();
        }
        [HttpGet("{cost}")]
        public IEnumerable<int> MaintenanceCostUnder(int cost)
        {
            return brandLogic.MaintenanceCostUnder(cost);
        }
        [HttpGet("{cost}")]
        public IEnumerable<int> MaintenanceCostAbowe(int cost)
        {
            return brandLogic.MaintenanceCostAbowe(cost);
        }
        [HttpGet("{cost}")]
        public int MCUnder(int cost)
        { 
            return brandLogic.MCUnder(cost);
        }
        [HttpGet("{cost}")]
        public int MCAbowe(int cost)
        {
            return brandLogic.MCAbowe(cost);
        }
    }
}
