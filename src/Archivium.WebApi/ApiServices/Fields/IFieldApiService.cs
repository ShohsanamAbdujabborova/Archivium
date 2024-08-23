using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Fields;

namespace Archivium.WebApi.ApiServices.Fields;

public interface IFieldApiService
{
    ValueTask<FieldViewModel> PostAsync(FieldCreateModel createModel);
    ValueTask<FieldViewModel> PutAsync(long id, FieldUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<FieldViewModel> GetAsync(long id);
    ValueTask<IEnumerable<FieldViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}