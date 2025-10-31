using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.API.Data;
using ProjectManager.API.DTOs;
using ProjectManager.API.Models;
using System.Security.Claims;

namespace ProjectManager.API.Controllers;

[Authorize]
[ApiController]
[Route("api/projects/{projectId}/[controller]")]
public class TasksController : ControllerBase {
  private readonly AppDbContext _db;
  public TasksController(AppDbContext db) => _db = db;

  private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

  [HttpGet]
  public async Task<IActionResult> Get(int projectId) {
    var uid = GetUserId();
    var project = await _db.Projects.Include(p => p.Tasks).SingleOrDefaultAsync(p => p.Id == projectId && p.UserId == uid);
    if (project == null) return NotFound();
    return Ok(project.Tasks);
  }

  [HttpPost]
  public async Task<IActionResult> Create(int projectId, [FromBody] ProjectManager.API.DTOs.TaskDto dto) {
    var uid = GetUserId();
    var project = await _db.Projects.SingleOrDefaultAsync(p => p.Id == projectId && p.UserId == uid);
    if (project == null) return NotFound();
    var t = new TaskItem { Title = dto.Title, DueDate = dto.DueDate, ProjectId = projectId };
    _db.Tasks.Add(t);
    await _db.SaveChangesAsync();
    return Ok(t);
  }

  [HttpPatch("{id}/toggle")]
  public async Task<IActionResult> Toggle(int projectId, int id) {
    var uid = GetUserId();
    var t = await _db.Tasks.Include(x => x.Project).SingleOrDefaultAsync(x => x.Id == id && x.Project!.UserId == uid);
    if (t == null) return NotFound();
    t.IsCompleted = !t.IsCompleted;
    await _db.SaveChangesAsync();
    return Ok(t);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int projectId, int id) {
    var uid = GetUserId();
    var t = await _db.Tasks.Include(x => x.Project).SingleOrDefaultAsync(x => x.Id == id && x.Project!.UserId == uid);
    if (t == null) return NotFound();
    _db.Tasks.Remove(t);
    await _db.SaveChangesAsync();
    return NoContent();
  }
}
