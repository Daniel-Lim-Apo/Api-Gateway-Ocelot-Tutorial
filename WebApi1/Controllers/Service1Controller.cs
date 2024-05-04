using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Service1Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing-WebApi1", "Bracing-WebApi1", "Chilly-WebApi1", "Cool-WebApi1", "Mild-WebApi1", "Warm", "Balmy", "Hot", "Sweltering", "Scorching-WebApi1"
        };

        private readonly ILogger<Service1Controller> _logger;

        public Service1Controller(ILogger<Service1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetService1")]
        public IEnumerable<Service1> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Service1
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
