namespace Archivium.WebApi.Models.Comments;

public class CommentCreateModel
{
    public string Content { get; set; }
    public DateTime Time { get; set; }
    public long ItemId { get; set; }
    public long UserId { get; set; }
    public long? ParentId { get; set; }
}
