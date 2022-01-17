using Compentio.Pipes.Client;
using Compentio.Pipes.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
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
            await _pipeClient.SendMessage(JsonSerializer.Serialize(note));

            // Do not stop for future messages
            //_pipeClient.Stop();

            return await Task.FromResult(new List<Note> { note });
        }        
    }
}
