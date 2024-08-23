using Archivium.Service.Helpers;
using System.Text.Json.Serialization;

namespace Archivium.Service.Configurations;

[JsonConverter(typeof(EnumStringConverter))]
public enum FileType
{
    Pictures = 1,
    Videos,
    Audios
}