using System;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace rabbit_recv
{
    class Program {

        private static readonly AutoResetEvent _closingEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
           
            var factory = new ConnectionFactory() { HostName = "192.168.0.103" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);


                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        Console.WriteLine($"running");
                        Thread.Sleep(1000);
                    }
                });

                Console.CancelKeyPress += ((s, a) =>
                {
                    Console.WriteLine("Bye!");
                    _closingEvent.Set();
                });

                _closingEvent.WaitOne();
            }
        }
    }
}
