using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize] // Protect this controller
[ApiController]
[Route("api/protected")]
public class ProtectedController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok(new { message = "This is a protected API endpoint." });
  }
}
