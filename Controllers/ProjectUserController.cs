using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Data;
using Outgo_tracker_Backend.Models;

namespace Outgo_tracker_Backend.Controllers
{
  public class ProjectUserController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ProjectUserController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/ProjectUser/GetUsersByProjectId/2
    [HttpGet("GetUsersByProjectId{projectId}")]

    public async Task<ActionResult<IEnumerable<string>>> GetProjectUserIds(int projectId)
    {
      var userIds = await _context.ProjectUsers
        .Where(pu => pu.ProjectId == projectId)
        .Select(pu => pu.UserId)
        .ToListAsync();

      if (userIds == null || !userIds.Any())
      {
        return NotFound();
      }

      return Ok(userIds);
    }

    // GET: api/ProjectUser/GetUsersByProjectId/2
    [HttpGet("GetProjectByUserId/{userId}")]

    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUserId(string userId)
    {
      var projects = await _context.ProjectUsers
        .Where(pu => pu.UserId == userId)
        .Select(pu => pu.Project)
        .ToListAsync();

      if (projects == null || !projects.Any())
      {
        return NotFound();
      }

      return Ok(projects);
    }

    // PUT: api/ProjectUser/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProjectUser(int id, ProjectUser projectUser)
    {
      if (id != projectUser.Id)
      {
        return BadRequest();
      }

      _context.Entry(projectUser).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProjectUserExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/ProjectUser
    [HttpPost]
    public async Task<ActionResult<ProjectUser>> PostProjectUser(ProjectUser projectUser)
    {
      _context.ProjectUsers.Add(projectUser);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProjectUser", new { id = projectUser.Id }, projectUser);
    }

    // DELETE: api/ProjectUser/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectUser(int id)
    {
      var projectUser = await _context.ProjectUsers.FindAsync(id);
      if (projectUser == null)
      {
        return NotFound();
      }

      _context.ProjectUsers.Remove(projectUser);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProjectUserExists(int id)
    {
      return _context.ProjectUsers.Any(e => e.Id == id);
    }
  }
}