using Archivium.Domain.Entities.Fields;
using Archivium.Service.Configurations;
using Archivium.Service.Services.FieldValues;
using Archivium.WebApi.Extensions;
using Archivium.WebApi.Models.FieldValues;
using Archivium.WebApi.Validators.FieldValues;
using AutoMapper;
using Org.BouncyCastle.Crypto;

namespace Archivium.WebApi.ApiServices.FieldValues;

public class FieldValueApiService(
    IMapper mapper,
    IFieldValueService fieldValueService,
    FieldValueCreateModelValidator createModelValidator,
    FieldValueUpdateModelValidator updateModelValidator) : IFieldValueApiService
{
    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await fieldValueService.DeleteAsync(id);
    }

    public async ValueTask<FieldValueViewModel> GetAsync(long id)
    {
        var fieldValue = await fieldValueService.GetByIdAsync(id);
        return mapper.Map<FieldValueViewModel>(fieldValue);
    }

    public async ValueTask<IEnumerable<FieldValueViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var fieldValues = await fieldValueService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<FieldValueViewModel>>(fieldValues);
    }

    public async ValueTask<FieldValueViewModel> PostAsync(FieldValueCreateModel createModel)
    {
        await createModelValidator.EnsureValidatedAsync(createModel);
        var mappedFieldValue = mapper.Map<FieldValue>(createModel);
        var createdFieldValue = await fieldValueService.CreateAsync(mappedFieldValue);
        return mapper.Map<FieldValueViewModel>(createdFieldValue);
    }

    public async ValueTask<FieldValueViewModel> PutAsync(long id, FieldValueUpdateModel updateModel)
    {
        await updateModelValidator.EnsureValidatedAsync(updateModel);
        var mappedFieldValue = mapper.Map<FieldValue>(updateModel);
        var createdFieldValue = await fieldValueService.UpdateAsync(id, mappedFieldValue);
        return mapper.Map<FieldValueViewModel>(createdFieldValue);
    }
}
