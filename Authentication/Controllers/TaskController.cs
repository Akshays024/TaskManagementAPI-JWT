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
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> GetTaskByStatus(string status="completed")
        {
            var result =await _task.GetTasksByStatus(status);
            if(result.Count == 0)
            {
                return NotFound("task not found");
            }
            return Ok(result);
        }
        [Authorize]
        [HttpGet("paged")]
        public async Task<IActionResult> GetTaskByPage(int pageNumber,int pageSize)
        {
            if(pageNumber<=0 || pageSize <= 0)
            {
                return BadRequest("page number and size must be greater than 0");
            }
            var result = await _task.GetTaskByPage(pageNumber, pageSize);
            if(result.Count == 0)
            {
                return NotFound("products not found");
            }
            return Ok(result);
        }
    }
}
