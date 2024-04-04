using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.CircuitBreakerProject.Model
{
    public class CircuitBreakerOpenException : Exception
    {
        public CircuitBreakerOpenException() : base("Circuit Breaker is open. Please wait...")
        {
        }
    }
}
