using Serialization.Interfaces;

namespace Serialization
{
    public class Analyzer<T>(ISerializer<T> serialiaer, IDeserializer<T> deserializer, ISpeedMeter<T> speedMeter)
    {
        private readonly ISerializer<T> _serializer = serialiaer; 
        private readonly IDeserializer<T> _deserializer = deserializer;
        private readonly ISpeedMeter<T> _speedMeter = speedMeter;
        public void CheckSerialization(T t, bool needOutput)
        {
            _speedMeter.CheckSerializationSpeed(_serializer.SerializeInLoop, t, needOutput);
        }

        public void CheckDeserialization(string serializedObj)
        {
            _speedMeter.CheckDeserializationSpeed(_deserializer.DeserializeInLoop, serializedObj);
        }
    }
}
