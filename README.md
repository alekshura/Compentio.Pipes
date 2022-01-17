# Compentio.Pipes
Provides interprocess communication between a pipe server and one or more pipe clients.

## When can be used
- Desktop applications: for example to notify GUI application from batch service worker (e.g. Windows Service). It is much faster than setup self-hosted API on GUI and request it via HTTP
 - You can consider use NamedPipes for MVP that can be developed and scaled in a future: you not need to install queues or event stream for asynchronous communication between processes.
 - Instead of WCF on one machine or over local network for duplex interprocess communication. Since .NET Core and .NET 6 do not offer server-side support for hosting WCF (actually it is recommended to [migrate WCF to gRPC](https://docs.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/migrate-wcf-to-grpc) or use [CoreWCF](https://github.com/CoreWCF/CoreWCF)) NamedPipes can be used for communication.

## How to use 
Just setup pipe cient and server in your apps and send command to server. 
In example below Web API app sends message to console application (It can be any app). 
In server app start `PipeServer` and setup it for listening for messages:

```csharp
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
```
In a client application create start pipe client `_pipeClient.Start()` (for simplicity it is done in `ApiController`):

```cs
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
```
and `IPipeClient` is registered in dependency injection container:

```cs
services.AddSingleton<IPipeClient>(factory => new PipeClient("TestPipe"));
```
with `TestPipe` name. It is important that your server also created with `TestPipe` name.

Here we simply can send message (or command) to another process or thread. This command is serialized on client and deserialized on the server. Pipe client and server here act as transport layer without binding to certain object types - developer is responsible to properly serialize and deserialize messages that defined in contracts assembly that is shared between applications. 
