using TaskStatus = WebAPI.Enums.TaskStatus;

namespace WebAPI.Entities;

public class ServiceTask
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long PublisherId { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}