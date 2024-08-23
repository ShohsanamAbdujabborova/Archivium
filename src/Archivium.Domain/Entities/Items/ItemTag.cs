using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Tags;

namespace Archivium.Domain.Entities.Items;
public class ItemTag : Auditable
{
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public long TagId { get; set; }
    public Tag Tag { get; set; }
}