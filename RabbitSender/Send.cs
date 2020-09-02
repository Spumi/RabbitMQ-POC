using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitSender
{
    public class Send
    {
        public void SendMessage(string message, string routingKey)
        {
            var factory = new ConnectionFactory() { HostName = "192.168.1.66" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                                 routingKey: routingKey,
                                                 basicProperties: null,
                                                 body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
