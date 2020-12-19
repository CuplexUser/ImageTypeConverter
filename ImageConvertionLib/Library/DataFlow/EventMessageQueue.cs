using System.Collections.Generic;

namespace ImageConverterLib.Library.DataFlow
{
    public class EventMessageQueue
    {
        private readonly Queue<string> _errorQueue;

        protected EventMessageQueue()
        {
            _errorQueue = new Queue<string>();
        }

        public void AddMessage(string message)
        {
            _errorQueue.Enqueue(message);
        }

        public IEnumerable<string> GetMessageEnumerable()
        {
            while (_errorQueue.Count > 0)
            {
                yield return _errorQueue.Dequeue();
            }
        }

        public bool IsEmpty => _errorQueue.Count == 0;

        public static EventMessageQueue CreateEventMessageQueue()
        {
            var messageQueue = new EventMessageQueue();

            return messageQueue;
        }
    }
}