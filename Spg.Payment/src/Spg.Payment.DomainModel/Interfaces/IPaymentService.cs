using Microsoft.AspNetCore.Mvc;
using Spg.Payment.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.DomainModel.Interfaces
{
    public interface IPaymentService
    {
        public Task<bool> IsPaid(Guid hashCode);
        public Task<DateTime> IsVerifiedTill(int id);
        public Task<int> CreatePayment(CreatePaymentDto createPaymentDto);
    }
}
