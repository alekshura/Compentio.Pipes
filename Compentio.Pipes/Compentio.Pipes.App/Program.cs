using Compentio.Pipes.Client;
using Compentio.Pipes.Server;
using System;

namespace Compentio.Pipes.App
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var _pipeServer = new PipeServer("TestPipe");
            _pipeServer.Start();
            _pipeServer.MessageReceivedEvent += (sender, args) => ReceiveMessage(sender, args);
            
            Console.ReadKey();
        }

        private static void ReceiveMessage(object sender, MessageReceivedEventArgs args)
        {
            if (args.Message == "StartBatchProcess")
            {
                Console.WriteLine(args.Message);
            }
        }
    }
}
