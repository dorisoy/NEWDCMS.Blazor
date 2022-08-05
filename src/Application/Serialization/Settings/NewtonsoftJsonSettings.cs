
using DCMS.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace DCMS.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}