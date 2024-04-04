using SPG.CircuitBreakerProject;
using SPG.CircuitBreakerProject.Model;
using SPG.Weather;

public class Program
{
    static void Main(string[] args)
    {
        var circuitBreaker = new CircuitBreaker();
        var weatherApiWebService = new WeatherApiWebService();
        var agricultureService = new AgricultureService(circuitBreaker, weatherApiWebService);

        while (true)
        {
            try
            {
                agricultureService.MakeDecision();
                Console.WriteLine("Circuit Breaker is closed.");
            }
            catch (CircuitBreakerOpenException)
            {
                Console.WriteLine("Circuit Breaker is open. Waiting before next call...");
                Thread.Sleep(10000);
                Console.WriteLine("Circuit Breaker is half-open. Trying the next call...");
            }
        }
    }
}



