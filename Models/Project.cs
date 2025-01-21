namespace Outgo_tracker_Backend.Models
{


  public class Project
  {
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int Budget { get; set; } = 0;
    public int Savings { get; set; } = 0;
    public Timeframe BudgetTimeframe { get; set; } = Timeframe.Monthly;
    // Navigation property
    public ICollection<ProjectUser> ProjectUsers { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<Income> Incomes { get; set; } = [];
  }

}