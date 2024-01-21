namespace ChervanevKV.Models;

public enum eTaskStatus
{
    New = 0,
    InProcess = 1,
    Complete = 2
}

public class WorkTask
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public eTaskStatus? Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public long ProjectID { get; set; }
    public List<long>? CommentsID { get; set; }

}
