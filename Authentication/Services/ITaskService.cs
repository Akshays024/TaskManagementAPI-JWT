using Authentication.Models;

namespace Authentication.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTask();
        Task<bool> AddTask(TaskItem task);
        Task<bool> UpdateTask(int id, TaskItem updatedTask);
        Task<bool> DeleteTask(int id);
        Task<List<TaskItem>> GetTasksByStatus(string Status="completed");
        Task<List<TaskItem>> GetTaskByPage(int pageNumber, int pageSize);
    }
}
