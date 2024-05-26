namespace PladauAPI.Models;

public class Post : BaseModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
    public int UpVote { get; set; }
    public int DownVote { get; set; }
    public string? PostId { get; set; }
}

