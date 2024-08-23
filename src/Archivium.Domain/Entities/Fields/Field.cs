using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Collections;
using Microsoft.VisualBasic.FileIO;

namespace Archivium.Domain.Entities.Fields;
public class Field : Auditable
{
    public string Name { get; set; }
    public FieldType FieldType { get; set; }
    public long CollectionId { get; set; }
    public Collection Collection { get; set; }
}