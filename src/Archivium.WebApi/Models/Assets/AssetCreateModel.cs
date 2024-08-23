using Archivium.Service.Configurations;

namespace Archivium.WebApi.Models.Assets;

public class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}
