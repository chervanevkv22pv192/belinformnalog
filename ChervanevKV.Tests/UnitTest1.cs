using ChervanevKV.Models;
using ChervanevKV.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Microsoft.Extensions.Options;
namespace ChervanevKV.Tests;

public class UnitTest1
{
    [Fact]
    public void TestAddingAndGettingInvolvedUsers()
    {
        Project project = new Project()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            TasksID = new(),
            InvolvedUsersID = new()
        };

        User user = new User()
        {
            Id = 1,
            Login = "Test",
            Name = "Test",
            Surname = "Test",
            Email = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };
        var options = new DbContextOptionsBuilder<ApplicationContext>();
        options.UseInMemoryDatabase("DB");
        ApplicationContext context = new ApplicationContext(options.Options);
        ProjectsController projectsController = new ProjectsController(context);
        UsersController usersController = new UsersController(context);
        _ = usersController.PostUser(user);
        _ = projectsController.PostProject(project);
        _ = usersController.PutUserToProject(1, user, 1);
        var result = projectsController.GetInvolvedUsers(1).Result.Value.ToJson();
        List<User> userList = [user];
        var correctResult = userList.ToJson();
        Assert.Equal(correctResult, result);
    }

    [Fact]
    public void TestDeletingInvolvedUsers()
    {
        Project project = new Project()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            TasksID = new(),
            InvolvedUsersID = new()
        };

        User user = new User()
        {
            Id = 1,
            Login = "Test",
            Name = "Test",
            Surname = "Test",
            Email = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };
        var options = new DbContextOptionsBuilder<ApplicationContext>();
        options.UseInMemoryDatabase("DB");
        ApplicationContext context = new ApplicationContext(options.Options);
        ProjectsController projectsController = new ProjectsController(context);
        UsersController usersController = new UsersController(context);
        _ = usersController.PostUser(user);
        _ = projectsController.PostProject(project);
        _ = usersController.PutUserToProject(1, user, 1);
        _ = usersController.DeleteUserFromProject(1, 1);
        var result = projectsController.GetInvolvedUsers(1).Result.Value.ToJson();
        List<User> userList = new();
        var correctResult = userList.ToJson();
        Assert.Equal(correctResult, result);
    }

    [Fact]
    public void TestGettingTasks()
    {
        Project project = new Project()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            TasksID = new(),
            InvolvedUsersID = new()
        };

        WorkTask task = new WorkTask()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            Status = eTaskStatus.New,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };

        var options = new DbContextOptionsBuilder<ApplicationContext>();
        options.UseInMemoryDatabase("DB");
        ApplicationContext context = new (options.Options);
        ProjectsController projectsController = new (context);
        WorkTasksController tasksController = new (context);
        _ = projectsController.PostProject(project);
        _ = tasksController.PostWorkTask(task, 1);
        var result = projectsController?.GetTasks(1)?.Result?.Value?[0].ProjectID;
        Assert.Equal(1, result);
    }


    [Fact]
    public void TestGettingTasksByStatus()
    {
        Project project = new Project()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            TasksID = new(),
            InvolvedUsersID = new()
        };

        WorkTask task = new WorkTask()
        {
            Id = 1,
            Name = "Test",
            Description = "Test",
            Status = eTaskStatus.New,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now
        };

        var options = new DbContextOptionsBuilder<ApplicationContext>();
        options.UseInMemoryDatabase("DB");
        ApplicationContext context = new(options.Options);
        ProjectsController projectsController = new(context);
        WorkTasksController tasksController = new(context);
        _ = projectsController.PostProject(project);
        _ = tasksController.PostWorkTask(task, 1);
        var result = projectsController?.GetTasksByStatus(1,0)?.Result?.Value?[0].ProjectID;
        Assert.Equal(1, result);
    }

}