using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Weather
{
    public class ApiNotReachableException : Exception
    {
        public ApiNotReachableException(string message) : base(message)
        {
        }
    }
}
