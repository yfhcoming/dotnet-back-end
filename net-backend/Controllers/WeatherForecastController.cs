using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using ServiceCount;

namespace net_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        [DllImport("Dll1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int add(int a, int b);

        [DllImport("LogWriter.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern char printLog();


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
        public IEnumerable<WeatherForecast> Get()
        {

            /*            var res = ClassPrinter.add(3, 4);
                        Console.WriteLine(res);*/

            var res = add(7, 9);
            Console.WriteLine(res);

            Console.WriteLine(printLog());

            var res2 = CountClass.addOne(2);
            Console.WriteLine(res2);


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
