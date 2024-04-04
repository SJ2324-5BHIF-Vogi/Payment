using SPG.CircuitBreaker.Model.State;
using SPG.CircuitBreaker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.CircuitBreaker
{
    public class CircuitBreaker
    {
        private int _failureCount = 0;
        private CircuitBreakerState _state;
        public TimeSpan Timeout { get; set; }
        public int Threshold { get; set; }

        public CircuitBreaker()
        {
            _state = new ClosedState(this);
            Timeout = TimeSpan.FromSeconds(30);
            Threshold = 3;
        }

        public bool IsThresholdReached()
        {
            return _failureCount >= Threshold;
        }

        public void IncreaseFailureCount()
        {
            _failureCount++;
        }

        public void ResetFailureCount()
        {
            _failureCount = 0;
        }

        public CircuitBreakerState MoveToOpenState()
        {
            _state = new OpenState(this);
            return _state;
        }

        public CircuitBreakerState MoveToHalfOpenState()
        {
            _state = new HalfOpenState(this);
            return _state;
        }

        public CircuitBreakerState MoveToClosedState()
        {
            _state = new ClosedState(this);
            return _state;
        }

        public void Execute(Action action)
        {
            _state.ProtectedCodeIsExecuting();

            try
            {
                action();
                _state.ProtectedCodeExecuted();
            }
            catch (Exception e)
            {
                _state.ActOnException(e);
                throw;
            }
        }
    }
}
