using Spg.Payment.DomainModel.Enums;
using System.Diagnostics;

namespace Spg.Payment.DomainModel.Model
{
    public class Payment
    {


        public int Id { get; private set; }
        public Guid Hash { get; set; }
        public int UserId { get; set; }
        public virtual User? UserNavigation { get; set; }
        public PaidForWhatEnum PaidForWhat { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        //public int PaidFromUserId { get; set; }
        //public virtual User? PaidFromUserNavigation { get; set; }
        public long Price { get; set; }
        public int Discount { get; set; }
        public bool Active { get; set; } = false;


        public Payment() { }
        public Payment(User user, PaidForWhatEnum paidForWhat, DateTime validFrom, DateTime validTill, long price, int discount)
        {
            UserId = user.Id;
            UserNavigation = user;
            PaidForWhat = paidForWhat;
            ValidFrom = validFrom;
            ValidTill = validTill;
            Hash = Guid.NewGuid();
            //PaidFromUserId = paidFromUser.Id;
            //PaidFromUserNavigation = paidFromUser;
            Price = price;
            Discount = discount;
        }
    }
}

