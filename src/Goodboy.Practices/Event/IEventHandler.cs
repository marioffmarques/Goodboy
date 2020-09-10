using System;
using System.Threading.Tasks;

namespace Goodboy.Practices.Event
{
    public interface IEventHandler<in T> where T : IEvent
    {
        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="event">Event.</param>
        Task HandleAsync(T @event);
    }
}
