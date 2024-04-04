using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.CircuitBreaker.Model.State
{
    public class HalfOpenState : CircuitBreakerState
    {
        public HalfOpenState(CircuitBreaker circuitBreaker)
        : base(circuitBreaker)
        { }

        public override void ProtectedCodeExecuted()
        {
            base.ProtectedCodeExecuted();
            _circuitBreaker.MoveToClosedState();
        }

        public override void ActOnException(Exception e)
        {
            base.ActOnException(e);
            _circuitBreaker.MoveToOpenState();
        }
    }
}
