using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceTaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public ServiceTaskController()
        {
            _taskService = new TaskService();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        [HttpGet("get{id}")]
        public IActionResult GetById(long id)
        {
            var task = _taskService.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpPost("create")]
        public IActionResult Create(ServiceTask task)
        {
            var createdTask = _taskService.CreateTask(task);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("update")]
        public IActionResult Update( ServiceTask task)
        {
            task.Id = task.Id;
            var updatedTask = _taskService.UpdateTask(task);
            if (updatedTask == null) return NotFound();
            return Ok(updatedTask);
        }
    }
}