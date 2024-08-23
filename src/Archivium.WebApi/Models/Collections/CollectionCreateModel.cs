namespace Archivium.WebApi.Models.Collections;

public class CollectionCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long UserId { get; set; }
    public long CategoryId { get; set; }
    public long? AssetId { get; set; }
}
