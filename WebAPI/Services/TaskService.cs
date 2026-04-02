using System.Text.Json;
using WebAPI.Entities;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class TaskService : ITaskService
{
    private readonly string _filePath = "C:\\Users\\Developr\\RiderProjects\\MSM_ERP_Test\\WebAPI\\DateBase\\tasks.json";

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
        task.CreatedDate = DateTime.Now; // Avtomatik vaqtni belgilash
        tasks.Add(task);
        SaveAllTasksToFile(tasks);
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
            existingTask.PublisherId = task.PublisherId;
            SaveAllTasksToFile(tasks);
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