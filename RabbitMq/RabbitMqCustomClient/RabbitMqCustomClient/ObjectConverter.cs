using Newtonsoft.Json;
using System.Text;

namespace RabbitMqCustomClient
{
    internal class ObjectConverter
    {
        public static byte[] ObjectToBytes<T>(T obj)
        {
            if (obj == null)
                return null;

            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        public static T BytesToObject<T>(byte[] bytes)
        {
            if (bytes == null)
                return default(T);

            var json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
