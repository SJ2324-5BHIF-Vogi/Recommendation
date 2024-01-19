using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG.Vogi.Recommendation.Application
{
    public class RabbitMqService
    {
        private readonly IModel _channel;

        public RabbitMqService(IModel channel)
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
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                return message;
            };

            _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
            return null;
        }
    }
}
