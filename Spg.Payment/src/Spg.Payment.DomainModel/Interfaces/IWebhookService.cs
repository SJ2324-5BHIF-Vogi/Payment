using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Payment.DomainModel.Interfaces
{
    public interface IWebhookService
    {
        public void HandleStripeWebhook(string json, string endpointSecret, string signature);
    }
}
