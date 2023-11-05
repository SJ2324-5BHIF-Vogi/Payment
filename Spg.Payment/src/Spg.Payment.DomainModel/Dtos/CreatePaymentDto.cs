using MediatR;
using Spg.Payment.DomainModel.Enums;

namespace Spg.Payment.DomainModel.Dtos 
{ 
    public class CreatePaymentDto: IRequest<Guid>
    {
        public PaidForWhatEnum PaidForWhat { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTill { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }

        public CreatePaymentDto (PaidForWhatEnum paidForWhat, DateTime validFrom, DateTime validTill, int price, int discount)
        {
            PaidForWhat = paidForWhat;
            ValidFrom = validFrom;
            ValidTill = validTill;
            Price = price;
            Discount = discount;
        }
    }
} 
