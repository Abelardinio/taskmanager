using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TaskManager.ServiceBus
{
    public class MessageSerializer : IMessageSerializer
    {
        public byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public object Deserialize(byte[] input)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(input, 0, input.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }
    }
}