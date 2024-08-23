using Archivium.Domain.Entities.Items;
using Archivium.Service.Configurations;

namespace Archivium.Service.Services.ItemTags;

public interface IItemTagService
{
    ValueTask<ItemTag> CreateAsync(ItemTag itemTag);
    ValueTask<ItemTag> UpdateAsync(long id, ItemTag itemTag);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<ItemTag> GetByIdAsync(long id);
    ValueTask<IEnumerable<ItemTag>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}
