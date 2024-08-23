using Archivium.Domain.Entities.Tags;
using Archivium.Service.Configurations;
using Archivium.Service.Services.Tags;
using Archivium.WebApi.Extensions;
using Archivium.WebApi.Models.Tags;
using Archivium.WebApi.Validators.Tags;
using AutoMapper;

namespace Archivium.WebApi.ApiServices.Tags;

public class TagApiService(
    IMapper mapper,
    ITagService tagService,
    TagCreateModelValidator createModelValidator,
    TagUpdateModelValidator updateModelValidator) : ITagApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await tagService.DeleteAsync(id);
    }

    public async ValueTask<TagViewModel> GetAsync(long id)
    {
        var tag = await tagService.GetByIdAsync(id);
        return mapper.Map<TagViewModel>(tag);
    }

    public async ValueTask<IEnumerable<TagViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var tags = await tagService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<TagViewModel>>(tags);
    }

    public async ValueTask<TagViewModel> PostAsync(TagCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedTag = mapper.Map<Tag>(createModel);
        var createdTag = await tagService.CreateAsync(mappedTag);
        return mapper.Map<TagViewModel>(createdTag);
    }

    public async ValueTask<TagViewModel> PutAsync(long id, TagUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedTag = mapper.Map<Tag>(updateModel);
        var createdTag = await tagService.UpdateAsync(id, mappedTag);
        return mapper.Map<TagViewModel>(createdTag);
    }
}
