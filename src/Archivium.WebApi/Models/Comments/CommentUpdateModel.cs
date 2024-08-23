namespace Archivium.WebApi.Models.Comments;

public class CommentUpdateModel
{
    public string Content { get; set; }
    public long ItemId { get; set; }
    public long UserId { get; set; }
    public long? ParentId { get; set; }
}
