using System.Threading.Tasks;
using Compentio.Pipes.Extensions;

namespace Compentio.Pipes.Client
{
    public interface IPipeClient : IPipeChannel
    {
        /// <summary>
        /// This method sends the given message asynchronously over the communication channel
        /// </summary>
        /// <param name="message">Comunication Message</param>
        /// <returns>A task of TaskResult</returns>
        Task<TaskResult> SendMessage(string message);
    }
}
