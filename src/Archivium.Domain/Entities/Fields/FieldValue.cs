using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Items;

namespace Archivium.Domain.Entities.Fields;

public class FieldValue : Auditable
{
    public string Value { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public long FieldId { get; set; }
    public Field Field { get; set; }
}