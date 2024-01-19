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
        public string Username { get; set; } = string.Empty;

        private List<Payments> _payments = new List<Payments>();
        public IReadOnlyList<Payments> Payments => _payments;

        public void AddPayment(Payments payment)
        {
            if(payment != null)
            {
                _payments.Add(payment);
            }
        }
    }
}