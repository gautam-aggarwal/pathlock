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
[Route("api/[controller]")]
public class ProjectsController : ControllerBase {
  private readonly AppDbContext _db;
  public ProjectsController(AppDbContext db) => _db = db;

  private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

  [HttpGet]
  public async Task<IActionResult> Get() {
    var uid = GetUserId();
    var projects = await _db.Projects.Where(p => p.UserId == uid).Include(p => p.Tasks).ToListAsync();
    return Ok(projects);
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] ProjectDto dto) {
    var uid = GetUserId();
    var p = new Project { Title = dto.Title, Description = dto.Description, UserId = uid };
    _db.Projects.Add(p);
    await _db.SaveChangesAsync();
    return Ok(p);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id) {
    var uid = GetUserId();
    var p = await _db.Projects.SingleOrDefaultAsync(x => x.Id == id && x.UserId == uid);
    if (p == null) return NotFound();
    _db.Projects.Remove(p);
    await _db.SaveChangesAsync();
    return NoContent();
  }
}
