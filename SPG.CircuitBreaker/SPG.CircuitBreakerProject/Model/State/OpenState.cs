using System;
namespace SPG.CircuitBreakerProject.Model.State
{
    public class OpenState : CircuitBreakerState
    {
        private readonly DateTime _openDateTime;

        public OpenState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            _openDateTime = DateTime.UtcNow;
        }

        public override CircuitBreaker ProtectedCodeIsExecuting()
        {
            base.ProtectedCodeIsExecuting();
            Update();
            return _circuitBreaker;
        }

        public override CircuitBreakerState Update()
        {
            base.Update();
            if (DateTime.UtcNow >= _openDateTime + _circuitBreaker.Timeout)
            {
                return _circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }
}
