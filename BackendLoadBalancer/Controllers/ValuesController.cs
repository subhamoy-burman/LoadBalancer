using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendLoadBalancer.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly List<string> servers = new List<string>
        {
            "https://localhost:7068",
            "http://localhost:5062",
            "http://localhost:5002"
        };

        private static int nextServer = 0;
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<string> Get()
        {
            var server = GetServer();
            var response = await client.GetAsync(server + Request.Path);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private string GetServer()
        {
            var server = servers[nextServer];
            nextServer = (nextServer + 1) % servers.Count;
            return server;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
