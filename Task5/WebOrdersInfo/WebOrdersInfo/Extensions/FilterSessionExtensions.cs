using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace WebOrdersInfo.Extensions
{
    public static class FilterSessionExtensions
    {
        public static T GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public static void SetData<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(value));
        }
    }
}