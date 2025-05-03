using Streaming.Entities;
using System.Collections.Concurrent;

namespace Streaming.Services
{
    public class SensorDataQueue
    {
        private readonly ConcurrentQueue<SensorData> _queue = new();

        public void Enqueue(SensorData data) => _queue.Enqueue(data);

        public bool TryDequeue(out SensorData data) => _queue.TryDequeue(out data);
    }
}
