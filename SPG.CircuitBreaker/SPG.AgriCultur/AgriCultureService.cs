
using SPG.CircuitBreakerProject.Model;
using SPG.Weather;

namespace SPG.AgriCultur
{
    public class AgriCultureService(CircuitBreaker circuitBreaker, WeatherApiWebService weatherApiWebService)
    {
        private readonly CircuitBreaker _circuitBreaker = circuitBreaker;
        private readonly WeatherApiWebService _weatherApiWebService = weatherApiWebService;

        public void MakeDecision()
        {
            _circuitBreaker.Execute(() => _weatherApiWebService.GetWeatherData());
        }
    }
} // hat sich nicht adden lassen als refernce in SPG.CircuitBreakerProject

