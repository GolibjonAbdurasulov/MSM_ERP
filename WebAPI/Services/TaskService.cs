using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Entities;
using WebAPI.Hubs;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class TaskService : ITaskService
{
    private readonly string _filePath;
    private readonly IHubContext<MonitoringHub> _hubContext;
    public TaskService(IHubContext<MonitoringHub> hubContext)
    {
        _hubContext = hubContext;
        
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(rootPath, "DateBase", "tasks.json");
        string directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
    private List<ServiceTask> GetAllTasksFromFile()
    {
        if (!File.Exists(_filePath)) return new List<ServiceTask>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<ServiceTask>>(json) ?? new List<ServiceTask>();
    }

    private void SaveAllTasksToFile(List<ServiceTask> tasks)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText(_filePath, json);
    }

    public ServiceTask CreateTask(ServiceTask task)
    {
        var tasks = GetAllTasksFromFile();
        task.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
        task.CreatedDate = DateTime.Now;
        tasks.Add(task);
        SaveAllTasksToFile(tasks);

        _hubContext.Clients.All.SendAsync("UpdateTasks"); 
        
        return task;
    }

    public ServiceTask UpdateTask(ServiceTask task)
    {
        var tasks = GetAllTasksFromFile();
        var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            SaveAllTasksToFile(tasks);

            _hubContext.Clients.All.SendAsync("UpdateTasks");
        }
        return existingTask;
    }
    public ServiceTask GetTaskById(long id)
    {
        return GetAllTasksFromFile().FirstOrDefault(t => t.Id == id);
    }

    public List<ServiceTask> GetTasks()
    {
        return GetAllTasksFromFile();
    }
}