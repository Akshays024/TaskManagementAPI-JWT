using Authentication.Models;

namespace Authentication.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTask();
        Task<bool> AddTask(TaskItem task);
        Task<bool> UpdateTask(int id, TaskItem updatedTask);
        Task<bool> DeleteTask(int id);
    }
}
