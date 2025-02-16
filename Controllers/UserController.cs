using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Data;
using Outgo_tracker_Backend.Models;

namespace Outgo_tracker_Backend.Controllers
{
  public class UserController : ControllerBase
  {

    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GetById: api/User/5

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
        return NotFound();
      }

      return user;
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = user.Id }, user);

    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(string id, User user)
    {
      if (id != user.Id)
      {
        return BadRequest();
      }

      _context.Entry(user).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserExists(id))
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

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user == null)
      {
        return NotFound();
      }

      _context.Users.Remove(user);

      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool UserExists(string id)
    {
      return _context.Users.Any(u => u.Id == id);
    }

  }
}