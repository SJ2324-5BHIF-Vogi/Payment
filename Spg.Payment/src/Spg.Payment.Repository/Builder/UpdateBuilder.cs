using Spg.Payment.DomainModel.Enums;
using Spg.Payment.DomainModel.Interfaces;
using Spg.Payment.DomainModel.Model;
using Spg.Payment.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.Repository.Builder
{
    public class UpdateBuilder<T> : IUpdateBuilder<T> where T : Payments
    {
        private readonly PaymentContext _paymentContext;
        public T data { get; set; }

        public UpdateBuilder(PaymentContext paymentContext, T data)
        {
            _paymentContext = paymentContext ;
            this.data = data;

        }
        public int Save()
        {
            return _paymentContext.SaveChanges();
        }

        public IUpdateBuilder<T> UpdatePaidForWhat(PaidForWhatEnum paidFor)
        {
            data.PaidForWhat = paidFor; 
            return this;
        }
    }
}
