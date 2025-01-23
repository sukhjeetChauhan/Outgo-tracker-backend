using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Models;
using Outgo_tracker_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Outgo_tracker_Backend.Controllers
{
  public class IncomeController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public IncomeController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Income/Project/{projectId}/Year
    [HttpGet("Project/{projectId}/Year")]
    public async Task<ActionResult<IEnumerable<Income>>> GetIncomesForYear(int projectId)
    {
      var currentYear = DateTime.Now.Year;
      var incomes = await _context.Incomes
                     .Where(i => i.ProjectId == projectId && i.Date.Year == currentYear)
                     .ToListAsync();
      return Ok(incomes);
    }


    //POST: api/Income
    [HttpPost]
    public async Task<ActionResult<Income>> PostIncome(Income income)
    {
      _context.Incomes.Add(income);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetIncome", new { id = income.Id }, income);
    }

    // PUT: api/Income/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutIncome(int id, Income income)
    {
      if (id != income.Id)
      {
        return BadRequest();
      }
      _context.Entry(income).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!IncomeExists(id))
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


    // DELETE: api/Income/5

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIncome(int id)
    {
      var income = await _context.Incomes.FindAsync(id);
      if (income == null)
      {
        return NotFound();
      }

      _context.Incomes.Remove(income);

      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool IncomeExists(int id)
    {
      return _context.Incomes.Any(e => e.Id == id);
    }
  }
}