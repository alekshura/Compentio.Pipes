using Compentio.Pipes.Contracts;
using Compentio.Pipes.Server;
using System;
using System.Text.Json;

namespace Compentio.Pipes.App
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var pipeServer = new PipeServer("TestPipe");
            pipeServer.Start();
            pipeServer.MessageReceivedEvent += (sender, args) => ReceiveMessage(sender, args);            
            Console.ReadKey();
        }

        private static void ReceiveMessage(object sender, MessageReceivedEventArgs args)
        {
            var message = JsonSerializer.Deserialize<Note>(args.Message);
            if (message is not null)
            {
                Console.WriteLine(args.Message);
            }
        }
    }
}
