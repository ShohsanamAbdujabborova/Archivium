using Archivium.Domain.Entities.Items;
using Archivium.Service.Configurations;

namespace Archivium.Service.Services.Items;

public interface IItemService
{
    ValueTask<Item> CreateAsync(Item item);
    ValueTask<Item> UpdateAsync(long id, Item item);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Item> GetByIdAsync(long id);
    ValueTask<IEnumerable<Item>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}