using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Fields;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Archivium.Service.Services.Fields;

public class FieldService(IUnitOfWork unitOfWork) : IFieldService
{
    public async ValueTask<Field> CreateAsync(Field field)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == field.CollectionId)
            ?? throw new NotFoundException($"Collection is not found with this ID={field.CollectionId}");

        var alreadyExistField = await unitOfWork.Fields.SelectAsync(f => f.Name.ToLower() == field.Name.ToLower() && f.FieldType == field.FieldType);
        if (alreadyExistField is not null)
            throw new AlreadyExistException("This field is already exist");

        field.CreatedByUserId = HttpContextHelper.UserId;
        var createdField = await unitOfWork.Fields.InsertAsync(field);
        createdField.Collection = existCollection;
        await unitOfWork.SaveAsync();

        return createdField;
    }

    public async ValueTask<Field> UpdateAsync(long id, Field field)
    {
        var existField = await unitOfWork.Fields.SelectAsync(f => f.Id == id)
            ?? throw new NotFoundException($"Field is not found with this ID={id}");

        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == field.CollectionId)
            ?? throw new NotFoundException($"Collection is not found with this ID={field.CollectionId}");

        var alreadyExistField = await unitOfWork.Fields.SelectAsync(c => c.Name.ToLower() == field.Name.ToLower() && c.FieldType == field.FieldType);
        if (alreadyExistField is not null)
            throw new AlreadyExistException("This field is already exist");

        existField.Name = field.Name;
        existField.FieldType = field.FieldType;
        existField.CollectionId = field.CollectionId;

        existCollection.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Fields.UpdateAsync(existField);
        await unitOfWork.SaveAsync();

        return existField;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existField = await unitOfWork.Fields.SelectAsync(f => f.Id == id)
            ?? throw new NotFoundException($"Field is not found with this ID={id}");

        existField.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Fields.DeleteAsync(existField);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Field> GetByIdAsync(long id)
    {
        var existField = await unitOfWork.Fields.SelectAsync(f => f.Id == id, includes: ["Collection"])
            ?? throw new NotFoundException($"Field is not found with this ID={id}");

        return existField;
    }

    public async ValueTask<IEnumerable<Field>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var fields = unitOfWork.Fields.SelectAsQueryable(includes: ["Collection"]).OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            fields = fields.Where(f => f.Name.ToLower().Contains(search.ToLower()));

        return await fields.ToPaginateAsQueryable(@params).ToListAsync();
    }
}