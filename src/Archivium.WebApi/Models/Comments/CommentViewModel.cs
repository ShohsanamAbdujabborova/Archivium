namespace Archivium.WebApi.Models.Comments;

public class CommentViewModel
{
    public long Id { get; set; }
    public string Content { get; set; }
    public DateTime Time { get; set; }
    public long ItemId { get; set; }
    public long UserId { get; set; }
    public long? ParentId { get; set; }
}
