using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Items;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.Items;

public class ItemService(IUnitOfWork unitOfWork) : IItemService
{
    public async ValueTask<Item> CreateAsync(Item item)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == item.CollectionId)
            ?? throw new NotFoundException($"Collection is not found with this ID={item.CollectionId}");

        var alreadyExistItem = await unitOfWork.Items.SelectAsync(i => i.CollectionId == item.CollectionId && i.Name.ToLower() == item.Name.ToLower());

        if (alreadyExistItem is not null)
            throw new AlreadyExistException($"This item is already exist");

        item.CreatedByUserId = HttpContextHelper.UserId;
        var createdItem = await unitOfWork.Items.InsertAsync(item);
        createdItem.Collection = existCollection;
        await unitOfWork.SaveAsync();

        return createdItem;
    }

    public async ValueTask<Item> UpdateAsync(long id, Item item)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(i => i.Id == item.CollectionId)
            ?? throw new NotFoundException($"Collection is not found with this ID={item.CollectionId}");

        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == id)
            ?? throw new NotFoundException($"Item is not found with this ID={id}");

        var alreadyExistItem = await unitOfWork.Items.SelectAsync(i => i.CollectionId == item.CollectionId && i.Name.ToLower() == item.Name.ToLower());

        if (alreadyExistItem is not null)
            throw new AlreadyExistException($"This item is already exist");

        existItem.CollectionId = item.CollectionId;

        existItem.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Items.UpdateAsync(existItem);
        existItem.Collection = existCollection;
        await unitOfWork.SaveAsync();

        return existItem;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == id)
            ?? throw new NotFoundException($"Item is not found with this ID={id}");
        existItem.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Items.DeleteAsync(existItem);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Item> GetByIdAsync(long id)
    {
        var existItem = await unitOfWork.Items.SelectAsync(expression: i => i.Id == id, includes: ["Collection", "ItemTags", "Likes", "FieldValues", "Comments"])
            ?? throw new NotFoundException($"Item is not found with this ID={id}");

        return existItem;
    }

    public async ValueTask<IEnumerable<Item>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var items = unitOfWork.Items.SelectAsQueryable(includes: ["Collection", "ItemTags", "Likes", "FieldValues", "Comments"]).OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            items = items.Where(i => i.Name.ToLower().Contains(search.ToLower()));
        return await items.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
