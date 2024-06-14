namespace FinalProject.Models.Comment;

public class CommentForGettingDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public int PostId { get; set; }
}
