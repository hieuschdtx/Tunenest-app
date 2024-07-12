using Newtonsoft.Json;

namespace tunenest.Domain.Extensions
{
    public static class JsonExtension
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public static T? FromJson<T>(this string json)
        {
            return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
