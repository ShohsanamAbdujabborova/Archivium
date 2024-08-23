using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Items;
using Archivium.Domain.Entities.Users;

namespace Archivium.Domain.Entities.Commets;

public class Comment : Auditable
{
    public string Content { get; set; }
    public DateTime Time { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long? ParentId { get; set; }
    public Comment Parent { get; set; }
    public ICollection<Comment> Replies { get; set; }
}