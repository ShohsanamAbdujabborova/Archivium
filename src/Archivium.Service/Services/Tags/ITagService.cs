using Archivium.Domain.Entities.Tags;
using Archivium.Service.Configurations;

namespace Archivium.Service.Services.Tags;

public interface ITagService
{
    ValueTask<Tag> CreateAsync(Tag tag);
    ValueTask<Tag> UpdateAsync(long id, Tag tag);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Tag> GetByIdAsync(long id);
    ValueTask<IEnumerable<Tag>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}