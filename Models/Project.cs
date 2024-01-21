namespace ChervanevKV.Models;

public class Project
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public List<long>? TasksID { get; set; }
    public List<long>? InvolvedUsersID { get; set; }
}
