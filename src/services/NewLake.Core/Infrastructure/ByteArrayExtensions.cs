using System.IO;
using System.Runtime.Serialization;

namespace NewLake.Core.Infrastructure
{
    public static class ByteArrayExtensions
    {
        public static byte[] SerializeToByteArray<T>(this T obj) where T : class
        {
            if (obj == null) { return null; }
            using var memoryStream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(memoryStream, obj);
            return memoryStream.ToArray();
        }

        public static T Deserialize<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null) { return default; }
            using var memoryStream = new MemoryStream(byteArray);
            var serializer = new DataContractSerializer(typeof(T));
            var obj = (T)serializer.ReadObject(memoryStream);
            return obj;
        }
    }
}
