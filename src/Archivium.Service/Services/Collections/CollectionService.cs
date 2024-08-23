using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Collections;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Archivium.Service.Services.Assets;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.Collections;

public class CollectionService(IUnitOfWork unitOfWork, IAssetService assetService) : ICollectionService
{
    public async ValueTask<Collection> CreateAsync(Collection collection)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == collection.UserId)
            ?? throw new NotFoundException($"User is not found with this ID={collection.UserId}");

        var existCategory = await unitOfWork.Categories.SelectAsync(c => c.Id == collection.CategoryId)
            ?? throw new NotFoundException($"Category is not found with this ID={collection.CategoryId}");

        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Name.ToLower() == collection.Name.ToLower());
        if (existCollection is not null)
            throw new AlreadyExistException("This collection already exists");

        collection.CreatedByUserId = HttpContextHelper.UserId;
        var createdCollection = await unitOfWork.Collections.InsertAsync(collection);
        await unitOfWork.SaveAsync();

        return createdCollection;
    }

    public async ValueTask<Collection> UpdateAsync(long id, Collection collection)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Collection is not found with this ID={id}");

        if (existCollection.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't edit collections you don't own");
        }
        var alreadyExistCollection = await unitOfWork.Collections.SelectAsync(c => c.Name.ToLower() == collection.Name.ToLower());
        if (alreadyExistCollection is not null)
            throw new AlreadyExistException("This collection already exists");

        existCollection.Name = collection.Name;
        existCollection.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Collections.UpdateAsync(collection);
        await unitOfWork.SaveAsync();

        return existCollection;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Collection is not found with this ID={id}");

        if (existCollection.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't edit collections you don't own");
        }

        existCollection.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Collections.DeleteAsync(existCollection);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Collection> GetByIdAsync(long id)
    {
        var existCollection = await unitOfWork.Collections.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Collection is not found with this ID={id}");

        return existCollection;
    }

    public async ValueTask<IEnumerable<Collection>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var collections = unitOfWork.Collections.SelectAsQueryable().OrderBy(filter);
        if (!string.IsNullOrEmpty(search))
            collections = collections.Where(c => c.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

        return await collections.ToPaginateAsQueryable(@params).ToListAsync();
    }

    public async ValueTask<Collection> UploadFileAsync(long id, IFormFile file)
    {
        await unitOfWork.BeginTransactionAsync();

        var existCollection = await unitOfWork.Collections
            .SelectAsync(c => c.Id == id, includes: ["User", "Category", "Asset", "Items", "Fields"])
            ?? throw new NotFoundException($"Collection is not found with this ID={id}");

        if (existCollection.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't edit collections you don't own");
        }

        var createdFile = await assetService.UploadAsync(file, FileType.Pictures);

        existCollection.Asset = createdFile;
        existCollection.AssetId = createdFile.Id;
        existCollection.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Collections.UpdateAsync(existCollection);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return existCollection;
    }

    public async ValueTask<Collection> DeleteFileAsync(long id)
    {
        await unitOfWork.BeginTransactionAsync();

        var existCollection = await unitOfWork.Collections
            .SelectAsync(c => c.Id == id, includes: ["User", "Category", "Asset", "Items", "Fields"])
            ?? throw new NotFoundException($"Collection is not found with this ID={id}");

        if (existCollection.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't edit collections you don't own");
        }

        await assetService.DeleteAsync(Convert.ToInt64(existCollection.AssetId));

        existCollection.AssetId = null;
        await unitOfWork.Collections.UpdateAsync(existCollection);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return existCollection;
    }
}