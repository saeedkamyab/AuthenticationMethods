using System.Text.Json;

namespace AuthApi.SpecialMethod.Extentions
{
    public static class ObjectExtensions
    {
        public static string SerializeEntity<T>(this  T data)
        {
            var serializedData = JsonSerializer.Serialize<object>(data, new JsonSerializerOptions
            {
                ReferenceHandler=customre
            });
        }
    }
}
