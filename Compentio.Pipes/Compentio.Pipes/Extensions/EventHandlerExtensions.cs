using System;

namespace Compentio.Pipes.Extensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// This method is a safe way to fire an event in a multithreaded process.
        /// Since there is a tiny chance that the event becomes null after the null check but before the invocation,
        /// we use this extension where the event is passed as an argument.
        /// Why is this helpful? MulticastDelagates are immutable, so if you first assign a variable, null check against the variable and invoke through it,
        /// you are safe
        /// </summary>
        /// <typeparam name="T">Parameter type</typeparam>
        /// <param name="event">Event</param>
        /// <param name="sender">Event sender</param>
        /// <param name="eventArgs">Event Args</param>
        public static void SafeInvoke<T>(this EventHandler<T> @event, object sender, T eventArgs)
            where T : EventArgs
        {
            @event?.Invoke(sender, eventArgs);
        }
    }
}
