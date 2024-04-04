using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.CircuitBreakerProject.Model.State
{
    public class ClosedState : CircuitBreakerState
    {
        public ClosedState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }

        public override void ActOnException(System.Exception e)
        {
            base.ActOnException(e);
            if (_circuitBreaker.IsThresholdReached())
            {
                _circuitBreaker.MoveToOpenState();
            }
        }
    }
}
