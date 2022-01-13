using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using Compentio.Pipes.Extensions;

namespace Compentio.Pipes.Client
{
    public class PipeClient : IPipeClient
    {
        private readonly NamedPipeClientStream _pipeClient;

        public PipeClient(string serverId)
        {
            _pipeClient = new NamedPipeClientStream(".", serverId, PipeDirection.InOut, PipeOptions.Asynchronous);
        }

        #region ICommunicationClient implementation

        /// <summary>
        /// Starts the client. Connects to the server.
        /// </summary>
        public void Start()
        {
            // 5 minutes
            const int tryConnectTimeout = 5 * 60 * 1000;
            if (!_pipeClient.IsConnected)
            {
                _pipeClient.Connect(tryConnectTimeout);
            }
        }

        /// <summary>
        /// Stops the client. Waits for pipe drain, closes and disposes it.
        /// </summary>
        public void Stop()
        {
            try
            {
                _pipeClient.WaitForPipeDrain();
            }
            finally
            {
                _pipeClient.Close();
                _pipeClient.Dispose();
            }
        }

        /// <summary>
        /// Sends string message to the server
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <returns>Async result</returns>
        public Task<TaskResult> SendMessage(string message)
        {
            var taskCompletionSource = new TaskCompletionSource<TaskResult>();

            if (_pipeClient.IsConnected)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                _pipeClient.BeginWrite(buffer, 0, buffer.Length, asyncResult =>
                {
                    try
                    {
                        taskCompletionSource.SetResult(EndWriteCallBack(asyncResult));
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.SetException(ex);
                    }
                }, null);
            }
            else
            {
                throw new IOException("pipe is not connected");
            }

            return taskCompletionSource.Task;
        }

        #endregion

        #region private methods

        /// <summary>
        /// This callback is called when the BeginWrite operation is completed.
        /// It can be called whether the connection is valid or not.
        /// </summary>
        /// <param name="asyncResult">Async Operation result</param>
        /// <returns>Task result</returns>
        private TaskResult EndWriteCallBack(IAsyncResult asyncResult)
        {
            _pipeClient.EndWrite(asyncResult);
            _pipeClient.Flush();

            return new TaskResult { IsSuccess = true };
        }

        #endregion
    }
}
