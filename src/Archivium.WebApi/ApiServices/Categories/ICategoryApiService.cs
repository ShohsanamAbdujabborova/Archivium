using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Categories;

namespace Archivium.WebApi.ApiServices;

public interface ICategoryApiService
{
    ValueTask<CategoryViewModel> PostAsync(CategoryCreateModel createModel);
    ValueTask<CategoryViewModel> PutAsync(long id, CategoryUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CategoryViewModel> GetAsync(long id);
    ValueTask<IEnumerable<CategoryViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}