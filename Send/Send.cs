using System.Text;
using System.Threading;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);




var timer = new Timer(sendIt, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

void sendIt(object? state)
{
    const string message = "Hello World #3!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);
    Console.WriteLine($" [x] Sent {message}");

}

