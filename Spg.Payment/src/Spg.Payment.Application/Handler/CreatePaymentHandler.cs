using MediatR;
using Spg.Payment.DomainModel.Dtos;
using Spg.Payment.DomainModel.Model;
using Spg.Payment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.Application.Handler
{
    public class CreatePaymentHandler : IRequestHandler<CreatePaymentDto, Guid>
    {
        private readonly GenericRepository _repository;

        public CreatePaymentHandler(GenericRepository repository)
        {
            _repository = repository;
        }

        public async Task<DomainModel.Model.Payment> Handle(CreatePaymentDto request, CancellationToken cancellationToken)
        {
            // Assuming you have a User entity or some way to identify the user
            var user = GetUserById(request.UserId);

            if (user == null)
            {
                // Handle the case where the user is not found
                // You might want to throw an exception or return an appropriate response
                throw new NotFoundException($"User with ID {request.UserId} not found.");
            }

            // Map CreatePaymentDto to your Payments entity
            DomainModel.Model.Payment payment = new DomainModel.Model.Payment(user, request.PaidForWhat, request.ValidFrom, request.ValidTill, request.Price, request.Discount);

            // Add the payment entity to the repository
            _repository.Add(payment);

            // Assuming your Payments class has an Id property
            return await Task.FromResult( payment);
        }

        // Dummy method to simulate retrieving a user by ID
        private User GetUserById(int userId)
        {
            return _repository.GetSingle<User>(userId);
        }

        Task<Guid> IRequestHandler<CreatePaymentDto, Guid>.Handle(CreatePaymentDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


