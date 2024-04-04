

namespace SPG.Weather
{
    public class WeatherApiWebService
    {
        private int _calls = 0;

        public void GetWeatherData()
        {
            _calls++;
            Console.WriteLine("Weather API-Call wurde durchgeführt!");

            for (int i = 0; i < 1000; i++)
            { }

            if (_calls % 10 == 0)
            {
                throw new ApiNotReachableException("Weather API is temporarily down! Please wait...");
            }
        }
    }
}
