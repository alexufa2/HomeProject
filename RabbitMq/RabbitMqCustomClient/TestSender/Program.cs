// See https://aka.ms/new-console-template for more information
using RabbitMqCustomClient;

Console.WriteLine("try send mesage");

var sender = new RabbitMQSender("localhost","/", 5672, "guest", "guest");
await sender.SendMessage(new { id = 1, text = "werwerwerwe" }, "amq.direct", "test_key");

Console.WriteLine("Press Enter");
Console.ReadLine();
