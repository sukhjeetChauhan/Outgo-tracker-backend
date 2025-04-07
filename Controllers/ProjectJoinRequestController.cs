using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Data;
using Outgo_tracker_Backend.Models; // Add this line

namespace Outgo_tracker_Backend.Controllers
{

  [Route("api/[controller]")]
  [ApiController]

  public class ProjectJoinRequestController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ProjectJoinRequestController(ApplicationDbContext context)
    {
      _context = context;

    }

    // Get: api/ProjectJoinRequest/ProjectId/5
    [HttpGet("ProjectId/{projectId}")]
    public async Task<ActionResult<ProjectJoinRequest>> GetProjectJoinRequestByProjectId(int projectId)
    {
      var projectJoinRequest = await _context.ProjectJoinRequests
        .FirstOrDefaultAsync(p => p.ProjectId == projectId);

      if (projectJoinRequest == null)
      {
        return NotFound();
      }

      return projectJoinRequest;
    }

    // Get: api/ProjectJoinRequest/UserId/5
    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<ProjectJoinRequest>> GetProjectJoinRequestByUserId(string userId)
    {
      var projectJoinRequest = await _context.ProjectJoinRequests
    .FirstOrDefaultAsync(p => p.UserId == userId);
    

      if (projectJoinRequest == null)
      {
        return NotFound();
      }

      return projectJoinRequest;
    }

    // POST: api/ProjectJoinRequest
    [HttpPost]

    public async Task<ActionResult<ProjectJoinRequest>> PostProjectJoinRequest(ProjectJoinRequest projectJoinRequest)
    
    {
      Console.WriteLine("PostProjectJoinRequest called");
      _context.ProjectJoinRequests.Add(projectJoinRequest);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetProjectJoinRequestByProjectId), new { projectId = projectJoinRequest.ProjectId }, projectJoinRequest);


    }

    // PUT: api/ProjectJoinRequest/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProjectJoinRequest(int id, ProjectJoinRequest projectJoinRequest)
    {
      if (id != projectJoinRequest.Id)
      {
        return BadRequest();
      }

      _context.Entry(projectJoinRequest).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProjectJoinRequestExists(id))
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

    // DELETE: api/ProjectJoinRequest/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectJoinRequest(int id)
    {
      var projectJoinRequest = await _context.ProjectJoinRequests.FindAsync(id);
      if (projectJoinRequest == null)
      {
        return NotFound();
      }

      _context.ProjectJoinRequests.Remove(projectJoinRequest);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProjectJoinRequestExists(int id)
    {
      return _context.ProjectJoinRequests.Any(e => e.Id == id);
    }
  }


}