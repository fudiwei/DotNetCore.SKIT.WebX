using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SKIT.WebX.RESTfulSample.Controllers
{
    using RESTful;

    [Route("weather-forecast")]
    public class WeatherForecastController : RESTfulControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RESTful.Paging.PagingQueryModel pagingQuery)
        {
            Random rng = new Random();
            IEnumerable<object> result = Enumerable
                .Range(1, 5)
                .Select(i => 
                {
                    dynamic model = new System.Dynamic.ExpandoObject();
                    model.date = DateTime.Now.AddDays(i);
                    model.temperatureC = rng.Next(-20, 55);
                    model.summary = Summaries[rng.Next(Summaries.Length)];
                    model.paging = pagingQuery;
                    return model;
                })
                .Skip((pagingQuery.Page - 1) * pagingQuery.Limit)
                .Take(pagingQuery.Limit)
                .ToArray<object>();
            return RESTfulPagingData(true, result);
        }
    }
}
