using Archivium.Service.Configurations;
using Archivium.WebApi.Models.FieldValues;

namespace Archivium.WebApi.ApiServices.FieldValues;

public interface IFieldValueApiService
{
    ValueTask<FieldValueViewModel> PostAsync(FieldValueCreateModel createModel);
    ValueTask<FieldValueViewModel> PutAsync(long id, FieldValueUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<FieldValueViewModel> GetAsync(long id);
    ValueTask<IEnumerable<FieldValueViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
