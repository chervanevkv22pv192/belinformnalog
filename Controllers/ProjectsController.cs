using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChervanevKV.Models;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace ChervanevKV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProjectsController(ApplicationContext context)
        {
            _context = context;
        }

        //Получение списка пользователей на проекте
        [HttpGet("{id}/InvolvedUsers")]
        public async Task<ActionResult<List<User>>> GetInvolvedUsers(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            List<User> involvedUsers = new();

            for (int i = 0; i < project.InvolvedUsersID?.Count; i++)
            {
                var user = await _context.Users.FindAsync(project.InvolvedUsersID[i]);
                if(user != null) 
                    involvedUsers.Add(user);
            }
            return involvedUsers;
        }

        //Получение списка задач на проекте
        [HttpGet("{id}/Tasks")]
        public async Task<ActionResult<List<WorkTask>>> GetTasks(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            List<WorkTask> tasks = new();

            for (int i = 0; i < project.TasksID?.Count; i++)
            {
                var task = await _context.WorkTasks.FindAsync(project.TasksID[i]);
                if (task != null)
                    tasks.Add(task);
            }
            return tasks;
        }

        //Получение списка задач на проекте по статусу
        [HttpGet("{id}/Tasks/{status}")]
        public async Task<ActionResult<List<WorkTask>>> GetTasksByStatus(long id, long status)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            List<WorkTask> tasks = new();

            for (int i = 0; i < project.TasksID?.Count; i++)
            {
                var task = await _context.WorkTasks.FindAsync(project.TasksID[i]);
                if (task != null && (long)task.Status == status)
                    tasks.Add(task);
            }
            return tasks;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(long id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            project.UpdateDate = DateTime.Now;

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            project.UpdateDate = project.CreationDate = DateTime.Now;

            _context.Projects.Add(project);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
