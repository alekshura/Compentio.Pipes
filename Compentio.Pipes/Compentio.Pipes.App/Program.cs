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
            pipeServer.MessageReceivedEvent += (sender, args) =>
            {
                if (args.Message == "FirstSynchronizationFinished" || args.Message == "ImagesSynchronizationFinished")
                {
                    Console.WriteLine(args.Message);
                }
                
            };

            Console.ReadKey();
        }
    }
}
