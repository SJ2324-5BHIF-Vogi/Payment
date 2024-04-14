using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace Spg.Payment.Application.Services
{
    public class RabbitService
    {
        private readonly RabbitMQ.Client.IModel _channel;

        public RabbitService(RabbitMQ.Client.IModel channel)
        {
            _channel = channel;
        }

        public void SendMessage(string queue, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
        }

        public string ReceiveMessage(string queue)
        {
            var consumer = new EventingBasicConsumer(_channel);
            string message = null;
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
            return message;
        }
    }
}
