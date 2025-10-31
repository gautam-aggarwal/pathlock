using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;
    public TasksController(TaskService service) => _service = service;

    [HttpGet]
    public IActionResult Get() => Ok(_service.GetAll());

    [HttpPost]
    public IActionResult Add([FromBody] TaskItem task) => Ok(_service.Add(task));

    [HttpPatch("{id}/toggle")]
    public IActionResult Toggle(int id)
    {
        var t = _service.Toggle(id);
        return t == null ? NotFound() : Ok(t);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) =>
        _service.Delete(id) ? NoContent() : NotFound();
}
