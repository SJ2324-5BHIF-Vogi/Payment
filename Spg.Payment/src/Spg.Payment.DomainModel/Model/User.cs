using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.DomainModel.Model
{
    public class User
    {
        public int Id { get; set; }

        public int MyUser { get; set; }

        private List<Payment> _payments = new List<Payment>();
        public IReadOnlyList<Payment> Payments => _payments;

        public void AddPayment(Payment payment)
        {
            if(payment != null)
            {
                _payments.Add(payment);
            }
        }

        public User()
        {
        }

        public User(int userId)
        {
            MyUser = userId;
        }
    }
}