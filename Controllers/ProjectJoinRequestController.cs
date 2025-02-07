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

    // Get: api/ProjectJoinRequest/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectJoinRequest>> GetProjectJoinRequest(int id)
    {
      var projectJoinRequest = await _context.ProjectJoinRequests.FindAsync(id);

      if (projectJoinRequest == null)
      {
        return NotFound();
      }

      return projectJoinRequest;
    }
  }


}