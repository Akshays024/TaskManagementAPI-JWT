using Authentication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Services
{
    public class TaskService : ITaskService
    {
        private static List<TaskItem> tasks = new List<TaskItem>()
        {
            new TaskItem{Id=1,Title="Asp.NetCore",Description="witch is used to work with api and backend",Status="completed"}
        };

        
        public async Task<List<TaskItem>> GetAllTask()
        {
            return await Task.FromResult<List<TaskItem>>(tasks);
        }

        public async Task<bool> AddTask(TaskItem task)
        {
            if(tasks.Any(t=>t.Id == task.Id))
            {
                return await Task.FromResult<bool>(false);
            }
            tasks.Add(task);
            return await Task.FromResult<bool>(true);
        } 

        public async Task<bool> UpdateTask(int id,TaskItem updatedTask)
        {

            var task = tasks.FirstOrDefault(t => t.Id == id);
            if(task != null)
            {
                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.Status = updatedTask.Status;
                return await Task.FromResult<bool>(true);
            }
            return await Task.FromResult<bool>(false);
        }
        public async Task<bool> DeleteTask(int id)
        {
            if (id <= 0)
            {
                return await Task.FromResult<bool>(false);
            }
            var task = tasks.FirstOrDefault(s => s.Id == id);
            if(task == null)
            {
                return await Task.FromResult<bool> (false);
            }
            tasks.Remove(task);
            return await Task.FromResult<bool>(true);
        }
        public async Task<List<TaskItem>> GetTasksByStatus(string Status = "completed")
        {
            var result = tasks.Where(t => t.Status == Status).ToList();
            return await Task.FromResult<List<TaskItem>>(result);
        }
        public async Task<List<TaskItem>> GetTaskByPage(int pageNumber, int pageSize)
        {
            var result = tasks
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
            return await Task.FromResult<List<TaskItem>>(result);
        }

    }
}
