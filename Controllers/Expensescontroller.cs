using Microsoft.EntityFrameworkCore;
using Outgo_tracker_Backend.Models;
using Outgo_tracker_Backend.Data;
using Microsoft.AspNetCore.Mvc;

namespace Outgo_tracker_Backend.Controllers
{
  public class Expensescontroller : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public Expensescontroller(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/Expenses/Project/{projectId}/Year
    [HttpGet("Project/{projectId}/Year")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesForYear(int projectId)
    {
      var currentYear = DateTime.Now.Year;
      var expenses = await _context.Expenses
                     .Where(e => e.ProjectId == projectId && e.Date.Year == currentYear)
                     .ToListAsync();
      return Ok(expenses);
    }

    // GET: api/Expenses/Project/{projectId}/Month
    [HttpGet("Project/{projectId}/Month")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesForMonth(int projectId)
    {
      var currentMonth = DateTime.Now.Month;
      var currentYear = DateTime.Now.Year;
      var expenses = await _context.Expenses
                     .Where(e => e.ProjectId == projectId && e.Date.Year == currentYear && e.Date.Month == currentMonth)
                     .ToListAsync();
      return Ok(expenses);
    }

    // GET: api/Expenses/Project/{projectId}/Week
    [HttpGet("Project/{projectId}/Week")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesForWeek(int projectId)
    {
      var currentDate = DateTime.Now;
      var startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek);
      var expenses = await _context.Expenses
                     .Where(e => e.ProjectId == projectId && e.Date >= startOfWeek && e.Date <= currentDate)
                     .ToListAsync();
      return Ok(expenses);
    }

    // GET: api/Expenses/Project/{projectId}/Past5Years
    [HttpGet("Project/{projectId}/Past5Years")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesForPast5Years(int projectId)
    {
      var currentDate = DateTime.Now;
      var past5YearsDate = currentDate.AddYears(-5);
      var expenses = await _context.Expenses
                     .Where(e => e.ProjectId == projectId && e.Date >= past5YearsDate && e.Date <= currentDate)
                     .ToListAsync();
      return Ok(expenses);
    }
    // PUT: api/Expenses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpense(int id, Expense expense)
    {
      if (id != expense.Id)
      {
        return BadRequest();
      }

      _context.Entry(expense).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ExpenseExists(id))
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

    // POST: api/Expenses
    [HttpPost]
    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    {
      _context.Expenses.Add(expense);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
    }

    // DELETE: api/Expenses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
      var expense = await _context.Expenses.FindAsync(id);
      if (expense == null)
      {
        return NotFound();
      }

      _context.Expenses.Remove(expense);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ExpenseExists(int id)
    {
      return _context.Expenses.Any(e => e.Id == id);
    }
  }
}