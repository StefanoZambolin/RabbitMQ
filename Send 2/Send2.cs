using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

const string message = "Hello World #2!";
var body = Encoding.UTF8.GetBytes(message);

Timer timer = new Timer(sendIt, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

void sendIt(object? state)
{
    channel.BasicPublish(exchange: string.Empty,
                         routingKey: "hello",
                         basicProperties: null,
                         body: body);
    Console.WriteLine($" [x] Sent {message}");


}


Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

