using Spg.Payment.DomainModel.Enums;

namespace Spg.Payment.DomainModel.Model
{
    public class Payment
    {
        public Payment(User user, PaidForWhatEnum paidForWhat, DateTime validFrom, DateTime validTill, User paidFromUser, int price, int discount)
        {
            UserId = user.Id;
            UserNavigation = user;
            PaidForWhat = paidForWhat;
            ValidFrom = validFrom;
            ValidTill = validTill;
            PaidFromUserId = paidFromUser.Id;
            PaidFromUserNavigation = paidFromUser;
            Price = price;
            Discount = discount;
        }

        public int Id { get; private set; }
        public HashCode Hash { get; set; } = new HashCode(); 
        public int UserId { get; set; }
        public virtual User UserNavigation { get; set; } 
        public PaidForWhatEnum PaidForWhat { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        public int PaidFromUserId { get; set; }
        public virtual User PaidFromUserNavigation { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public bool Active { get; set; } = false;
    }
}
