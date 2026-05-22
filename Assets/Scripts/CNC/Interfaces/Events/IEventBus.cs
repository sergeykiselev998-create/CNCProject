using System;
namespace CNC.Interfaces.Events
{
    /// <summary>
    /// Event bus interface for tool-related events
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribe to an event of type T.
        /// </summary>
        /// <param name="handler">Handler method</param>
        /// <param name="priority">Call priority. Lower number = higher priority (called first).</param>
        /// <returns>IDisposable for unsubscribing</returns>
        IDisposable Subscribe<T>(Action<T> handler, int priority = 0) where T : IEventMessage;
    
        /// <summary>
        /// Publish an event.
        /// </summary>
        void Publish<T>(T evt) where T : IEventMessage;
    }
}




