using System;
namespace Goodboy.Practices.Event
{
    /// <summary>
    /// Rejected information
    /// </summary>
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }

        string Code { get; }
    }
}
