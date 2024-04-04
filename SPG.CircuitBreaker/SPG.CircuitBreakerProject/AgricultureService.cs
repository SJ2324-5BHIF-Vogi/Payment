using SPG.CircuitBreakerProject.Model;
using SPG.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.CircuitBreakerProject
{
    public class AgricultureService
    {
        private readonly CircuitBreaker _circuitBreaker;
        private readonly WeatherApiWebService _weatherApiWebService;

        public AgricultureService(CircuitBreaker circuitBreaker, WeatherApiWebService weatherApiWebService)
        {
            _circuitBreaker = circuitBreaker;
            _weatherApiWebService = weatherApiWebService;
        }

        public void MakeDecision()
        {
            _circuitBreaker.Execute(() => _weatherApiWebService.GetWeatherData());
        }
    }
}
