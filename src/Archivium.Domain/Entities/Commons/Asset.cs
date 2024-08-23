using Archivium.Domain.Commons;

namespace Archivium.Domain.Entities.Commons;
public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
