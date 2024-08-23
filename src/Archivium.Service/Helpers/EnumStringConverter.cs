using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archivium.Service.Helpers;

public class EnumStringConverter : StringEnumConverter
{
    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}