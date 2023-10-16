using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PaidForWhatEnum
{
    None, // No Subscription
    Monthly, // one Month
    Yearly // twelve Months 
};

namespace Spg.Payment.DomainModel.Model
{
    public class Payment
    {
        public Payment(HashCode hash, User? userNavigation, DateTime validFrom,
            DateTime validTill, User? paidFromUserId, float price, float discount, bool active)
        {
            Hash = hash;
            UserNavigation = userNavigation;
            ValidFrom = validFrom;
            ValidTill = validTill;
            PaidFromUserId = paidFromUserId;
            Price = price;
            Discount = discount;
            Active = active;
        }

        public int PaymentId { get; set; }
        public HashCode Hash { get; set; } = new HashCode(); 
        private PaidForWhatEnum PaidForWhat { get; set; } = PaidForWhatEnum.None;
        public virtual User UserNavigation { get; set; } 
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        public User? PaidFromUserId { get; set; }
        public int Price { get; set; }
        public float Discount { get; set; }
        public bool Active { get; set; }
    }
}
