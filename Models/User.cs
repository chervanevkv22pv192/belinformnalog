namespace ChervanevKV.Models;

public class User
{
    public long Id { get; set; }
    public string? Login { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
