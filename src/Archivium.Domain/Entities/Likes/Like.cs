using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Items;
using Archivium.Domain.Entities.Users;

namespace Archivium.Domain.Entities.Likes;

public class Like : Auditable
{
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}