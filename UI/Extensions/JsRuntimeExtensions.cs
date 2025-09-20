using Microsoft.JSInterop;
using System.Text.Json;
using UI.Json;

namespace Microsoft.JSInterop
{
    public static class JsRuntimeExtensions
    {
        public static async Task InvokeVoidWithOriginalCaseAsync(this IJSRuntime jsRuntime, string identifier, object? arg)
        {
            var json = JsonSerializer.Serialize(arg, OriginalCaseJsonSerializerOptions.Options);
            var args = new object[] { JsonSerializer.Deserialize<JsonElement>(json) };
            await jsRuntime.InvokeVoidAsync(identifier, args);
        }
    }
}
