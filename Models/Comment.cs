namespace ChervanevKV.Models;

public class Comment
{
    public long Id { get; set; }
    public string? Text { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public long TaskId { get; set; }
}
