using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Items;

namespace Archivium.Domain.Entities.Tags;
public class Tag : Auditable
{
    public string Name { get; set; }
    public IEnumerable<ItemTag> ItemTags { get; set; }
}