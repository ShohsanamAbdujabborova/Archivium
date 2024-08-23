using Archivium.Domain.Entities.Collections;
using Archivium.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Archivium.Service.Services.Collections;

public interface ICollectionService
{
    ValueTask<Collection> CreateAsync(Collection collection);
    ValueTask<Collection> UpdateAsync(long id, Collection collection);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Collection> GetByIdAsync(long id);
    ValueTask<IEnumerable<Collection>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<Collection> UploadFileAsync(long id, IFormFile file);
    ValueTask<Collection> DeleteFileAsync(long id);
}