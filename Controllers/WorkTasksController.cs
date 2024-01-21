using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChervanevKV.Models;
using Microsoft.CodeAnalysis;

namespace ChervanevKV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTasksController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public WorkTasksController(ApplicationContext context)
        {
            _context = context;
        }

        //Получение комментариев к задаче
        [HttpGet("{id}/Comments")]
        public async Task<ActionResult<List<Comment>>> GetComments(long id)
        {
            var task = await _context.WorkTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            List<Comment> comments = new();

            for (int i = 0; i < task.CommentsID?.Count; i++)
            {
                var comment = await _context.Comments.FindAsync(task.CommentsID[i]);
                if (comment != null)
                    comments.Add(comment);
            }
            return comments;
        }

        // GET: api/WorkTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkTask>>> GetWorkTasks()
        {
            return await _context.WorkTasks.ToListAsync();
        }

        // GET: api/WorkTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkTask>> GetWorkTask(long id)
        {
            var workTask = await _context.WorkTasks.FindAsync(id);

            if (workTask == null)
            {
                return NotFound();
            }

            return workTask;
        }

        // PUT: api/WorkTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTask(long id, WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return BadRequest();
            }

            workTask.UpdateDate = DateTime.Now;

            _context.Entry(workTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTaskExists(id))
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

        // POST: api/WorkTasks/AddToProject/projectID
        // Добавление задачи к проекту
        [HttpPost("AddToProject/{projectId}")]
        public async Task<ActionResult<WorkTask>> PostWorkTask(WorkTask workTask, long projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);

            if (project == null)
            {
                return BadRequest();
            }

            workTask.ProjectID = projectId;
            workTask.UpdateDate = workTask.CreationDate = DateTime.Now;

            _context.WorkTasks.Add(workTask);

            project.TasksID.Add(workTask.Id);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkTask), new { id = workTask.Id }, workTask);
        }

        // DELETE: api/WorkTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTask(long id)
        {
            var workTask = await _context.WorkTasks.FindAsync(id);
            
            if (workTask == null)
            {
                return NotFound();
            }

            var project = _context.Projects.Where(p => p.TasksID.Contains(id)).FirstOrDefault();
            if(project != default)
            {
                project.TasksID.Remove(id);
                project.UpdateDate = DateTime.Now;
            }

            _context.WorkTasks.Remove(workTask);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkTaskExists(long id)
        {
            return _context.WorkTasks.Any(e => e.Id == id);
        }
    }
}
