using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Spg.Payment.Application.Handler;
using Spg.Payment.DomainModel.Dtos;
using Spg.Payment.DomainModel.Interfaces;
using Spg.Payment.DomainModel.Model;
using Spg.Payment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly GenericRepository _repository;

        public PaymentService(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreatePayment(CreatePaymentDto createPaymentDto)
        {
            // Assuming you have a User entity or some way to identify the user
            var user = _repository.GetSingle<User>(createPaymentDto.UserId);


            if (user == null)
            {
                // Handle the case where the user is not found
                User newUser = new(createPaymentDto.UserId);

                _repository.Add(newUser);

                user = newUser;
            }

            // Map CreatePaymentDto to your Payments entity
            DomainModel.Model.Payment payment = new DomainModel.Model.Payment(user, createPaymentDto.PaidForWhat, createPaymentDto.ValidFrom, createPaymentDto.ValidTill, user, createPaymentDto.Price, createPaymentDto.Discount);

            // Add the payment entity to the repository
            _repository.Add(payment);

            // Assuming your Payments class has an Id property
            return payment.Id;
        }
        public async Task<bool> IsPaid(Guid hashCode)
        {
            var all = _repository.GetAll<Payment.DomainModel.Model.Payment>();
            var payment = all.FirstOrDefault(p => p.Hash.ToString() == hashCode.ToString());

            if (payment == null)
            {
                // Handle the case where the user is not found
                throw new NotFoundException($"Payment with Hash {hashCode} not found.");
            }

            return payment.Active;
        }

        public async Task<DateTime> IsVerifiedTill(int userId)
        {
            var user = _repository.GetSingle<User>(userId);

            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            var validTill = user.Payments.Where(p => p.ValidTill > DateTime.Now).ToList();

            return validTill.Max(p => p.ValidTill);
        }
    }
}
