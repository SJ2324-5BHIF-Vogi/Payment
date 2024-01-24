using MediatR;
using Spg.Payment.DomainModel.Enums;
using Spg.Payment.DomainModel.Model;

namespace Spg.Payment.DomainModel.Dtos 
{ 
    public class CreatePaymentDto: IRequest<Guid>
    {

        public int UserId { get; set; }
        public PaidForWhatEnum PaidForWhat { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }

        public CreatePaymentDto (int userId, PaidForWhatEnum paidForWhat, DateTime validFrom, DateTime validTill, int price, int discount)
        {
            UserId = userId;
            PaidForWhat = paidForWhat;
            ValidFrom = validFrom;
            ValidTill = validTill;
            Price = price;
            Discount = discount;
        }
    }
} 
