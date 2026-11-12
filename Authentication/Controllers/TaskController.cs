using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _task;
        public TaskController(ITaskService service)
        {
            _task = service;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _task.GetAllTask();
            return Ok(tasks);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskItem task)
        {
            if (task.Id <= 0)
            {
                return BadRequest("id must be greater than 0");
            }
            var result = await _task.AddTask(task);
            if (!result)
            {
                return BadRequest("Task already exist");

            }
            return CreatedAtAction(nameof(GetAllTask),task);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id,TaskItem updatedTask)
        {
            if (updatedTask.Id <= 0)
            {
                return BadRequest("id must be greater than 0");
            }
            var result = await _task.UpdateTask(id, updatedTask);
            if (!result)
            {
                return NotFound("task not found");
            }
            return NoContent();
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _task.DeleteTask(id);
            if (!task)
            {
                return NotFound("task not found");
            }
            return NoContent();

        }
    }
}
