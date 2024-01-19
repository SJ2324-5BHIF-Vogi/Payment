using Spg.Payment.DomainModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.DomainModel.Interfaces
{
    public interface IUpdateBuilder<T> where T : class
    {
        int Save();
        IUpdateBuilder<T> UpdatePaidForWhat(PaidForWhatEnum paidFor);
    }   
}
