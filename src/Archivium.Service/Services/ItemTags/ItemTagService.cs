using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Items;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.ItemTags;

public class ItemTagService(IUnitOfWork unitOfWork) : IItemTagService
{
    public async ValueTask<ItemTag> CreateAsync(ItemTag itemTag)
    {
        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == itemTag.ItemId)
            ?? throw new NotFoundException($"Item is not found with this ID={itemTag.ItemId}");

        var existTag = await unitOfWork.Tags.SelectAsync(t => t.Id == itemTag.TagId)
            ?? throw new NotFoundException($"Tag is not found with this ID={itemTag.TagId}");

        itemTag.CreatedByUserId = HttpContextHelper.UserId;

        var createdItemTag = await unitOfWork.ItemTags.InsertAsync(itemTag);
        createdItemTag.Item = existItem;
        createdItemTag.Tag = existTag;
        await unitOfWork.SaveAsync();

        return createdItemTag;
    }

    public async ValueTask<ItemTag> UpdateAsync(long id, ItemTag itemTag)
    {
        var existItemTag = await unitOfWork.ItemTags.SelectAsync(it => it.Id == id)
            ?? throw new NotFoundException($"ItemTag is not found with this ID={id}");

        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == itemTag.ItemId)
            ?? throw new NotFoundException($"Item is not found with this ID={itemTag.ItemId}");

        var existTag = await unitOfWork.Tags.SelectAsync(t => t.Id == itemTag.TagId)
            ?? throw new NotFoundException($"Tag is not found with this ID={itemTag.TagId}");

        existItemTag.ItemId = itemTag.ItemId;
        existItemTag.TagId = itemTag.TagId;
        existItemTag.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.ItemTags.UpdateAsync(existItemTag);
        await unitOfWork.SaveAsync();

        return existItemTag;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existItemTag = await unitOfWork.ItemTags.SelectAsync(it => it.Id == id)
            ?? throw new NotFoundException($"ItemTag is not found with this ID={id}");

        existItemTag.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.ItemTags.DeleteAsync(existItemTag);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<ItemTag> GetByIdAsync(long id)
    {
        var existItemTag = await unitOfWork.ItemTags.SelectAsync(it => it.Id == id, includes: ["Item", "Tag"])
            ?? throw new NotFoundException($"ItemTag is not found with this ID={id}");

        return existItemTag;
    }

    public async ValueTask<IEnumerable<ItemTag>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var itemTags = unitOfWork.ItemTags.SelectAsQueryable(includes: ["Item", "Tag"]).OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            itemTags = itemTags.Where(it => it.Tag.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await itemTags.ToPaginateAsQueryable(@params).ToListAsync();
    }
}
