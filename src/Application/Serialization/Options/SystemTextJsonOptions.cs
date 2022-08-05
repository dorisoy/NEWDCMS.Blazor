using DCMS.Application.Interfaces.Serialization.Options;
using System.Text.Json;

namespace DCMS.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}