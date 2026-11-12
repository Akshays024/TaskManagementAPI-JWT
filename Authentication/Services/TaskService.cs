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
            return tasks;
        }

        public async Task<bool> AddTask(TaskItem task)
        {
            
            foreach(var T in tasks)
            {
                if(T.Id == task.Id)
                {
                    return false;
                }
            }
            tasks.Add(task);
            return true;
        } 

        public async Task<bool> UpdateTask(int id,TaskItem updatedTask)
        {
            
            bool isFound = false;
            foreach(var task in tasks)
            {
                if(task.Id == id)
                {
                    task.Title = updatedTask.Title;
                    task.Description = updatedTask.Description;
                    task.Status = updatedTask.Status;
                    isFound = true;
                    break;
                }
            }
            if (isFound)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteTask(int id)
        {
            if (id <= 0)
            {
                return false;
            }
            var task = tasks.FirstOrDefault(s => s.Id == id);
            if(task == null)
            {
                return false;
            }
            tasks.Remove(task);
            return true;
        }

    }
}
