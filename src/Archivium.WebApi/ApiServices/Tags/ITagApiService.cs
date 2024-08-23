using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Tags;

namespace Archivium.WebApi.ApiServices.Tags;

public interface ITagApiService
{
    ValueTask<TagViewModel> PostAsync(TagCreateModel createModel);
    ValueTask<TagViewModel> PutAsync(long id, TagUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<TagViewModel> GetAsync(long id);
    ValueTask<IEnumerable<TagViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
