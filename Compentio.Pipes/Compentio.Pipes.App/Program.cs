using Compentio.Pipes.Server;
using System;

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
            if (args.Message == "MessageToSend")
            {
                Console.WriteLine(args.Message);
            }
        }
    }
}
