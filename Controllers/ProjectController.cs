using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Data;
using Outgo_tracker_Backend.Models;

namespace Outgo_tracker_Backend.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
      return await _context.Projects.ToListAsync();
    }

    // GET: api/Project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
      var project = await _context.Projects.FindAsync(id);

      if (project == null)
      {
        return NotFound();
      }

      return project;
    }

    //Get: api/Project/byname/{name}
    [HttpGet("byname/{name}")]
    public async Task<ActionResult<int>> GetProjectByName(string name)
    {
      var project = await _context.Projects.FirstOrDefaultAsync(p => p.Name == name);

      if (project == null)
      {
        return NotFound();
      }

      return project.Id;
    }



    // GET: api/Project/role/{userId}/{role}
    [HttpGet("role/{userId}/{role}")]

    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsWithRole(string userId, string role)
    {
      // Try to parse the string to your Role enum
      if (!Enum.TryParse<Role>(role, true, out var parsedRole))
      {
        return BadRequest("Invalid role");
      }

      var projects = await _context.Projects
          .Where(p => p.ProjectUsers
              .Any(pu => pu.UserId == userId && pu.Role == parsedRole))
          .ToListAsync();

      if (projects == null || !projects.Any())
      {
        return NotFound();
      }

      return projects;
    }

    // PUT: api/Project/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(int id, Project project)
    {
      if (id != project.Id)
      {
        return BadRequest();
      }

      _context.Entry(project).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProjectExists(id))
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

    // POST: api/Project
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Project>> PostProject(Project project)
    {
      // Check if a project with the same name already exists
      bool nameExists = await _context.Projects
          .AnyAsync(p => p.Name.ToLower() == project.Name.ToLower());

      if (nameExists)
      {
        return Conflict(new { message = "A project with this name already exists." });
      }

      _context.Projects.Add(project);
      await _context.SaveChangesAsync();

      return Ok(project);
    }


    // DELETE: api/Project/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Project>> DeleteProject(int id)
    {
      var project = await _context.Projects.FindAsync(id);
      if (project == null)
      {
        return NotFound();
      }

      _context.Projects.Remove(project);
      await _context.SaveChangesAsync();

      return project;
    }

    private bool ProjectExists(int id)
    {
      return _context.Projects.Any(e => e.Id == id);
    }
  }

}