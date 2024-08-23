using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Fields;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.FieldValues;

public class FieldValueService(IUnitOfWork unitOfWork) : IFieldValueService
{
    public async ValueTask<FieldValue> CreateAsync(FieldValue fieldValue)
    {
        var existField = await unitOfWork.Fields.SelectAsync(f => f.Id == fieldValue.FieldId)
            ?? throw new NotFoundException($"Field is not found with this ID={fieldValue.FieldId}");

        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == fieldValue.ItemId)
            ?? throw new NotFoundException($"Item is not found with this ID={fieldValue.ItemId}");

        var alreadyExistFieldValue = await unitOfWork.FieldValues.SelectAsync(f => f.FieldId == fieldValue.FieldId && f.ItemId == fieldValue.ItemId && f.Value.ToLower() == fieldValue.Value.ToLower());
        if (alreadyExistFieldValue is not null)
            throw new AlreadyExistException($"This field value is already exist");

        fieldValue.CreatedByUserId = HttpContextHelper.UserId;
        var createdFieldValue = await unitOfWork.FieldValues.InsertAsync(fieldValue);
        createdFieldValue.Field = existField;
        createdFieldValue.Item = existItem;
        await unitOfWork.SaveAsync();

        return createdFieldValue;
    }

    public async ValueTask<FieldValue> UpdateAsync(long id, FieldValue fieldValue)
    {
        var existFieldValue = await unitOfWork.FieldValues.SelectAsync(fv => fv.Id == id)
            ?? throw new NotFoundException($"FieldValue is not found with this ID={id}");

        var existField = await unitOfWork.Fields.SelectAsync(f => f.Id == fieldValue.FieldId)
            ?? throw new NotFoundException($"Field is not found with this ID={fieldValue.FieldId}");

        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == fieldValue.ItemId)
            ?? throw new NotFoundException($"Item is not found with this ID={fieldValue.ItemId}");

        var alreadyExistFieldValue = await unitOfWork.FieldValues.SelectAsync(f => f.FieldId == fieldValue.FieldId && f.ItemId == fieldValue.ItemId && f.Value.ToLower() == fieldValue.Value.ToLower());
        if (alreadyExistFieldValue is not null)
            throw new AlreadyExistException($"This field value is already exist");

        existFieldValue.Value = fieldValue.Value;
        existFieldValue.FieldId = fieldValue.FieldId;
        existFieldValue.ItemId = fieldValue.ItemId;
        existFieldValue.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.FieldValues.UpdateAsync(existFieldValue);
        await unitOfWork.SaveAsync();

        return existFieldValue;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existFieldValue = await unitOfWork.FieldValues.SelectAsync(fv => fv.Id == id)
            ?? throw new NotFoundException($"FieldValue is not found with this ID={id}");

        existFieldValue.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.FieldValues.DeleteAsync(existFieldValue);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<FieldValue> GetByIdAsync(long id)
    {
        var existFieldValue = await unitOfWork.FieldValues.SelectAsync(fv => fv.Id == id, includes: ["Field", "Item"])
            ?? throw new NotFoundException($"FieldValue is not found with this ID={id}");

        return existFieldValue;
    }

    public async ValueTask<IEnumerable<FieldValue>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var fieldValues = unitOfWork.FieldValues.SelectAsQueryable(includes: ["Field", "Item"]).OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            fieldValues = fieldValues.Where(fv => fv.Value.ToLower().Contains(search.ToLower()));

        return await fieldValues.ToPaginateAsQueryable(@params).ToListAsync();
    }
}