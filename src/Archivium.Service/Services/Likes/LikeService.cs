using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Likes;
using Archivium.Service.Exceptions;
using Archivium.Service.Helpers;


namespace Archivium.Service.Services.Likes;

public class LikeService(IUnitOfWork unitOfWork) : ILikeService
{
    public async ValueTask<Like> CreateAsync(Like like)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == like.UserId)
            ?? throw new NotFoundException($"User is not found with this ID={like.UserId}");

        var existItem = await unitOfWork.Items.SelectAsync(i => i.Id == like.ItemId)
            ?? throw new NotFoundException($"Item is not found with this ID={like.ItemId}");

        var existLike = await unitOfWork.Likes.SelectAsync(l => l.UserId == like.UserId && l.ItemId == like.ItemId)
            ?? throw new AlreadyExistException($"This like is already exist");

        like.CreatedByUserId = HttpContextHelper.UserId;
        var createdLike = await unitOfWork.Likes.InsertAsync(like);
        createdLike.User = existUser;
        createdLike.Item = existItem;
        await unitOfWork.SaveAsync();

        return createdLike;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existLike = await unitOfWork.Likes.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"This like is not found with this ID={id}");

        if (existLike.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't delete likes you don't create");
        }
        existLike.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Likes.DeleteAsync(existLike);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Like>> GetAllAByItemIdsync(long itemId)
    {
        var item = await unitOfWork.Items.SelectAsync(i => i.Id == itemId)
            ?? throw new NotFoundException($"Item is not found with this Id={itemId}");

        return await unitOfWork.Likes.SelectAsEnumerableAsync(expression: l => l.ItemId == itemId, includes: ["User", "Item"], isTracked: false);
    }

    public async ValueTask<IEnumerable<Like>> GetAllAByUserIdsync(long userId)
    {
        var user = await unitOfWork.Users.SelectAsync(i => i.Id == userId)
            ?? throw new NotFoundException($"User is not found with this Id={userId}");

        return await unitOfWork.Likes.SelectAsEnumerableAsync(expression: l => l.UserId == userId, includes: ["User", "Item"], isTracked: false);
    }
}
