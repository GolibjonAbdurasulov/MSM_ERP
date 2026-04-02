using WebAPI.Entities;

namespace WebAPI.Interfaces;

public interface ITaskService
{
    public ServiceTask CreateTask(ServiceTask task);
    public ServiceTask UpdateTask(ServiceTask task);
    public ServiceTask GetTaskById(long id);
    public List<ServiceTask> GetTasks();
}