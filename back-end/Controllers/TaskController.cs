using todo.Models.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo;
using Task = todo.Models.Tasks.Task;

namespace todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskContext _tasksContext;
        public TasksController(TaskContext tasksContext)
        {
            _tasksContext = tasksContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasksByUserId(int id)
        {
            if (_tasksContext.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _tasksContext.Tasks.Where(t => t.IdUser == id).OrderByDescending(d => d.Id).ToListAsync();

            return tasks.Any() ? Ok(tasks) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateTask(Task task)
        {

            _tasksContext.Tasks.Add(task);
            var row = await _tasksContext.SaveChangesAsync();

            return Ok(row);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _tasksContext.Entry(task).State = EntityState.Modified;
            try
            {
                await _tasksContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            if (_tasksContext.Tasks == null)
            {
                return NotFound();
            }
            var task = await _tasksContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            task.Removed = true;
            await _tasksContext.SaveChangesAsync();
            return Ok();
        }
    }
}
