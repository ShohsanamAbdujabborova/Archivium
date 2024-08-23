using Archivium.DataAccess.UnitOfWorks;
using Archivium.Domain.Entities.Commets;
using Archivium.Service.Configurations;
using Archivium.Service.Exceptions;
using Archivium.Service.Extensions;
using Archivium.Service.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Archivium.Service.Services.Comments;

public class CommentService(IUnitOfWork unitOfWork) : ICommentService
{
    public async ValueTask<Comment> CreateAsync(Comment comment)
    {
        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == comment.UserId)
            ?? throw new NotFoundException($"User is not found with this Id = {comment.UserId}");

        var existItem = await unitOfWork.Items.SelectAsync(item => item.Id == comment.ItemId)
             ?? throw new NotFoundException($"Item is not found with this ID = {comment.ItemId}");

        comment.ParentId = comment.ParentId == 0 ? null : comment.ParentId;
        comment.CreatedByUserId = HttpContextHelper.UserId;
        var createdComment = await unitOfWork.Comments.InsertAsync(comment);
        await unitOfWork.SaveAsync();

        createdComment.Item = existItem;
        createdComment.User = existUser;
        return createdComment;
    }

    public async ValueTask<Comment> UpdateAsync(long id, Comment comment)
    {
        var existComment = await unitOfWork.Comments.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Comment is not found with this Id = {id}");

        var existUser = await unitOfWork.Users.SelectAsync(user => user.Id == comment.UserId)
            ?? throw new NotFoundException($"User is not found with this Id = {comment.UserId}");

        var existItem = await unitOfWork.Items.SelectAsync(item => item.Id == comment.ItemId)
             ?? throw new NotFoundException($"Item is not found with this ID = {comment.ItemId}");

        if (existComment.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't edit comments you don't write");
        }

        existComment.Content = comment.Content;
        existComment.UpdatedByUserId = HttpContextHelper.UserId;

        await unitOfWork.Comments.UpdateAsync(existComment);
        await unitOfWork.SaveAsync();
        existComment.Item = existItem;
        existComment.User = existUser;

        return existComment;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existComment = await unitOfWork.Comments.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Comment is not found with this Id = {id}");

        if (existComment.UserId != HttpContextHelper.UserId)
        {
            throw new UnauthorizedAccessException("You can't delete comments you don't write");
        }

        existComment.DeletedByUserId = HttpContextHelper.UserId;
        await unitOfWork.Comments.DeleteAsync(existComment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Comment> GetByIdAsync(long id)
    {
        var existComment = await unitOfWork.Comments
            .SelectAsync(expression: c => c.Id == id, includes: new[] { "Item", "User", "Parent", "Replies" })
            ?? throw new NotFoundException($"Comment is not found with this Id = {id}");

        return existComment;
    }

    public async ValueTask<IEnumerable<Comment>> GetAllAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var comments = unitOfWork.Comments
            .SelectAsQueryable(includes: new[] { "Item", "User", "Parent", "Replies" }, isTracked: false)
            .OrderBy(filter);

        if (!string.IsNullOrWhiteSpace(search))
            comments = comments.Where(c => c.Content.ToLower().Contains(search.ToLower()));

        return await comments.ToPaginateAsQueryable(@params).ToListAsync();
    }
}

