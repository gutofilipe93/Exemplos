using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HangFireInMemory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IBackgroundJobClient _backgroundJobs;
        private static CancellationTokenSource _resetCacheToken;
        
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBackgroundJobClient backgroundJobs)
        {
            _logger = logger;
            _backgroundJobs = backgroundJobs;
            _resetCacheToken = new CancellationTokenSource(); 
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _backgroundJobs.Enqueue(() => TestTempo());            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            
        }

        [HttpGet]
        [Route("reser")]
        public string Reset()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();

            return "Done";
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void TestTempo()
        {
            Thread.Sleep(TimeSpan.FromSeconds(15));
            Console.WriteLine("Hello world from Hangfire!");
        }
    }
}
