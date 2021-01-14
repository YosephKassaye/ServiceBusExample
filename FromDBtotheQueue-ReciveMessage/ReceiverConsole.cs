using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FromDBtotheQueue_ReciveMessage
{
    class ReceiverConsole
    {
        static string ConnectionString = "";
        static string QueuePath = "";
        static void Main(string[] args)
        {
            var queueClient = new QueueClient(ConnectionString, QueuePath);

            queueClient.RegisterMessageHandler(ProcessMessagesAsync, HandleExceptionAsync);

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
            queueClient.CloseAsync().Wait();

        }

        private static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var content = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"Recived: {content}");
        }

        private static Task HandleExceptionAsync(ExceptionReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }

       
    }
}
