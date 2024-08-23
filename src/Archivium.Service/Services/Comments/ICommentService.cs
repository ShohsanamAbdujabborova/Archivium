using Archivium.Domain.Entities.Commets;
using Archivium.Service.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivium.Service.Services.Comments;

public interface ICommentService
{
    ValueTask<Comment> CreateAsync(Comment comment);
    ValueTask<Comment> UpdateAsync(long id, Comment comment);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Comment> GetByIdAsync(long id);
    ValueTask<IEnumerable<Comment>> GetAllAsync(PaginationParams @params, Filter filter, string search = null);
}