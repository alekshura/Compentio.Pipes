using Compentio.Pipes.Server;
using Compentio.Pipes.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compentio.Pipes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IPipeServer _pipeServer;

        public NotesController(IPipeServer pipeServer)
        {
            _pipeServer = pipeServer;
            pipeServer.Start();
            pipeServer.MessageReceivedEvent += (sender, args) => ReceiveMessage(sender, args);
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Note
            {
                Id = index,
                Title = $"Title {index}",
                Value = $"Value {index}"
            })
            .ToArray();
        }

        private void ReceiveMessage(object? sender, MessageReceivedEventArgs args)
        {
            if (args.Message == "FirstSynchronizationFinished" || args.Message == "ImagesSynchronizationFinished")
            {
                Console.WriteLine(args.Message);
            }
        }
    }
}
