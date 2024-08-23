using Archivium.Service.Configurations;
using Archivium.WebApi.Models.Comments;

namespace Archivium.WebApi.ApiServices.Comments;

public interface ICommentApiService
{
    ValueTask<CommentViewModel> PostAsync(CommentCreateModel createModel);
    ValueTask<CommentViewModel> PutAsync(long id, CommentUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<CommentViewModel> GetAsync(long id);
    ValueTask<IEnumerable<CommentViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null);
}
