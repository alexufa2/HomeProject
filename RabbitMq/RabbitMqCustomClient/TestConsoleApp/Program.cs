using RabbitMqCustomClient;

namespace TestConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sender = new RabbitMqSender<Message>("localhost", "/", 5672, "contracts_creator", "contrat_cr_Pass_rabbit");

            var consumer = new RabbitMqConsumer<Message>("localhost", "/", 5672, "contracts_creator", "contrat_cr_Pass_rabbit");
            var consumerTask = consumer.StartConsumerAsync("contract.created.queue",
                                                            (msg) => {Console.WriteLine($"{msg.id} - {msg.text}");}
                );
            consumerTask.Wait();

            Console.WriteLine("Enter message");
            var msg = Console.ReadLine();

            var task = sender.SendMessage(new Message { id = 1, text = msg }, "to.checker", "contract.created");
            task.Wait();

            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        private class Message
        { 
            public int id;
            public string text;
        }
    }
}
