using Microsoft.EntityFrameworkCore;
namespace ChervanevKV.Models;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options){}
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<WorkTask> WorkTasks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}