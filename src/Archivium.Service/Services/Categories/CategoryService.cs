using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Categories;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.Categories;

public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    public async ValueTask<Category> CreateAsync(Category category)
    {
        var existCategory = await unitOfWork.Categories.SelectAsync(c => c.Name.ToLower() == category.Name.ToLower());
        if (existCategory is not null)
            throw new AlreadyExistException("This category is already exists with this name");

        category.CreatedByUserId = HttpContextHelper.UserId;
        var createdCategory = await unitOfWork.Categories.InsertAsync(category);
        await unitOfWork.SaveAsync();

        return createdCategory;
    }

    public async ValueTask<Category> UpdateAsync(long id, Category category)
    {
        var existCategory = await unitOfWork.Categories.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Category is not found with this Id={id}");

        var alreadyExistCategrory = await unitOfWork.Categories.SelectAsync(c => c.Name.ToLower() == category.Name.ToLower());
        if (alreadyExistCategrory is not null)
            throw new AlreadyExistException("This category already exists");

        existCategory.Name = category.Name;
        existCategory.UpdatedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Categories.UpdateAsync(existCategory);
        await unitOfWork.SaveAsync();

        return existCategory;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existCategory = await unitOfWork.Categories.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Category is not found with this ID={id}");

        existCategory.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Categories.DeleteAsync(existCategory);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Category> GetByIdAsync(long id)
    {
        var existCategory = await unitOfWork.Categories.SelectAsync(c => c.Id == id, includes: ["Collections"])
            ?? throw new NotFoundException($"Category is not found with this ID={id}");

        return existCategory;
    }

    public async ValueTask<IEnumerable<Category>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var categories = unitOfWork.Categories.SelectAsQueryable().OrderBy(filter);

        if (!string.IsNullOrEmpty(search))
            categories = categories.Where(c => c.Name.Contains(search, StringComparison.Ordinal));

        return await categories.ToPaginateAsQueryable(@params).ToListAsync();
    }
}