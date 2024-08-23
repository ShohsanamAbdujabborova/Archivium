using Archivium.Domain.Entities.Items;
using Archivium.Service.Configurations;
using Archivium.Service.Services.Items;
using Archivium.WebApi.Extensions;
using Archivium.WebApi.Models.Items;
using Archivium.WebApi.Validators.Items;
using AutoMapper;
using Org.BouncyCastle.Crypto;

namespace Archivium.WebApi.ApiServices.Items;

public class ItemApiService(
    IMapper mapper,
    IItemService itemService,
    ItemCreateModelValidator createModelValidator,
    ItemUpdateModelValidator updateModelValidator) : IItemApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await itemService.DeleteAsync(id);
    }

    public async ValueTask<ItemViewModel> GetAsync(long id)
    {
        var item = await itemService.GetByIdAsync(id);
        return mapper.Map<ItemViewModel>(item);
    }

    public async ValueTask<IEnumerable<ItemViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var items = await itemService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<ItemViewModel>>(items);
    }

    public async ValueTask<ItemViewModel> PostAsync(ItemCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedItem = mapper.Map<Item>(createModel);
        var createdItem = await itemService.CreateAsync(mappedItem);
        return mapper.Map<ItemViewModel>(createdItem);
    }

    public async ValueTask<ItemViewModel> PutAsync(long id, ItemUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedItem = mapper.Map<Item>(updateModel);
        var createdItem = await itemService.UpdateAsync(id, mappedItem);
        return mapper.Map<ItemViewModel>(createdItem);
    }
}
