using Archivium.Domain.Entities.Commons;
using Archivium.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Archivium.Service.Services.Assets;

public interface IAssetService
{
    ValueTask<Asset> UploadAsync(IFormFile file, FileType type);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Asset> GetByIdAsync(long id);
}