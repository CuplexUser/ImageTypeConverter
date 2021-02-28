using System.Collections.Generic;

namespace ImageConverterLib.Library.DataFlow
{
    /// <summary>
    ///    EventMessageQueue
    /// </summary>
    public class EventMessageQueue
    {
        /// <summary>
        /// The error queue
        /// </summary>
        private readonly Queue<string> _errorQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMessageQueue"/> class.
        /// </summary>
        protected EventMessageQueue()
        {
            _errorQueue = new Queue<string>();
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void AddMessage(string message)
        {
            _errorQueue.Enqueue(message);
        }

        /// <summary>
        /// Gets the message enumerable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetMessageEnumerable()
        {
            while (_errorQueue.Count > 0)
            {
                yield return _errorQueue.Dequeue();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty => _errorQueue.Count == 0;

        /// <summary>
        /// Creates the event message queue.
        /// </summary>
        /// <returns></returns>
        public static EventMessageQueue CreateEventMessageQueue()
        {
            var messageQueue = new EventMessageQueue();

            return messageQueue;
        }
    }
}