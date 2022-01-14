using Compentio.Pipes.Client;
using Compentio.Pipes.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compentio.Pipes.WebApi.Controllers
{
    [ApiController]
    [Route("notes")]
    public class NotesController : ControllerBase
    {
        private readonly IPipeClient _pipeClient;

        public NotesController(IPipeClient pipeClient)
        {
            _pipeClient = pipeClient;
            _pipeClient.Start();            
        }

        [HttpGet]
        public async Task<IEnumerable<Note>> Get()
        {
            var note = new Note { Id = 1, Title = "Title", Value = "Value" };
            await _pipeClient.SendMessage("MessageToSend");

            //_pipeClient.Stop();

            return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new Note
            {
                Id = index,
                Title = $"Title {index}",
                Value = $"Value {index}"
            })
            .ToArray());
        }        
    }
}
