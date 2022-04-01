using System.Text.Json;

namespace Inventory.Shared
{
    public interface IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonOptions { get; }
    }

    public class JsonSerialOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonOptions => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            AllowTrailingCommas = true
        };
    }
}
