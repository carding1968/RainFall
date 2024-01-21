using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RainfallAPI;

namespace RainfallAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        

        

        private readonly ILogger<RainfallController> _logger;

        public RainfallController(ILogger<RainfallController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "get-rainfall")]
        public async Task<List<Items>> GetAsync(string stationId)
        {
            List<Items> client = await new ApiClient().RunAsync(stationId);

            return client;
        }
    }
}




