using MediatR;
using Spg.Payment.DomainModel.Dtos;
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
        public CreatePaymentHandler()
        {
        }

        public Task<Guid> Handle(CreatePaymentDto request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Guid());
        }
    }
}
