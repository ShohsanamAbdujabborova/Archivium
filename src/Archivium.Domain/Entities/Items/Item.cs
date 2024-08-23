using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Collections;
using Archivium.Domain.Entities.Commets;
using Archivium.Domain.Entities.Fields;
using Archivium.Domain.Entities.Likes;

namespace Archivium.Domain.Entities.Items;

public class Item : Auditable
{
    public string Name { get; set; }
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
    public IEnumerable<ItemTag> ItemTags { get; set; }
    public IEnumerable<Like> Likes { get; set; }
    public IEnumerable<FieldValue> FieldValues { get; set; }
    public IEnumerable<Comment> Comments { get; set; }
}