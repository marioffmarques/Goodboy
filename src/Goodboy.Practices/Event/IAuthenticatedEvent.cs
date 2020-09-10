using System;
namespace Goodboy.Practices.Event
{
    /// <summary>
    /// Event interface containing authenticated user info
    /// </summary>
    public class IAuthenticatedEvent : IEvent
    {
        Guid UserId { get; set; }
    }
}
