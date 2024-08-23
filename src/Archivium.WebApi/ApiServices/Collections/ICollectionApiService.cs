using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Collections;

namespace Archivium.WebApi.ApiServices.Collections;

public interface ICollectionApiService
{
    ValueTask<CollectionViewModel> PostAsync(CollectionCreateModel createModel);
    ValueTask<CollectionViewModel> PutAsync(long id, CollectionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CollectionViewModel> GetAsync(long id);
    ValueTask<IEnumerable<CollectionViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
    ValueTask<CollectionViewModel> UploadPictureAsync(long id, IFormFile picture);
    ValueTask<CollectionViewModel> DeletePictureAsync(long id);
}