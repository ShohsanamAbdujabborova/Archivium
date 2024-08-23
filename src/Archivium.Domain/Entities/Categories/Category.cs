using Archivium.Domain.Commons;
using Archivium.Domain.Entities.Collections;
using System.Security.AccessControl;

namespace Archivium.Domain.Entities.Categories;

public class Category : Auditable
{
    public string Name { get; set; }
    public IEnumerable<Collection> Collections { get; set; }
}
