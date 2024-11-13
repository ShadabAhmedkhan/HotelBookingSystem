using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HotelBookingSystem.Application.Helper
{
    public class Serializer
    {
        public static string Serialize(object value, bool typeNameHandling = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { DateFormatString = SystemSettings.JsonDateTimePattern, ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

            if (typeNameHandling)
            {
                settings.TypeNameHandling = TypeNameHandling.Objects;
            }

            string json = JsonConvert.SerializeObject(value, settings);

            return json;
        }

        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, DateFormatString = SystemSettings.ShortDatePattern };

            settings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = SystemSettings.LongDatePattern });

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static object Deserialize(string json, Type type)
        {
            if (string.IsNullOrEmpty(json))
            {
                return new object();
            }

            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, DateFormatString = SystemSettings.ShortDatePattern };

            settings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = SystemSettings.LongDatePattern });

            return JsonConvert.DeserializeObject(json, type, settings);
        }

        public static T GetPropertyValue<T>(string json, string propertyPath)
        {
            JObject jObject = JObject.Parse(json);
            JToken jToken = jObject.SelectToken(propertyPath);

            return jToken.ToObject<T>();
        }

        public static T DeepCopy<T>(T @object, bool typeNameHandling = true)
        {
            string json = Serialize(@object, typeNameHandling);

            return Deserialize<T>(json);
        }
    }
}
