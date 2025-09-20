using System.Text.Json;
using System.Text.Json.Serialization;

namespace UI.Json
{
    public static class OriginalCaseJsonSerializerOptions
    {
        public static JsonSerializerOptions Options => new()
        {
            PropertyNamingPolicy = null, // Preserve original property names
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
