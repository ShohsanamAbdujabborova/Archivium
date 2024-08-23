using Archivium.WebApi.Models.Likes;

namespace Archivium.WebApi.ApiServices.Likes;

public interface ILikeApiService
{
    ValueTask<LikeViewModel> PostAsync(LikeCreateModel createModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<IEnumerable<LikeViewModel>> GetAllAByUserIdsync(long UserId);
    ValueTask<IEnumerable<LikeViewModel>> GetAllAByItemIdsync(long ItemId);
}
