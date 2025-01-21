namespace Outgo_tracker_Backend.Models
{

  public class Expense
  {
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public string? Description { get; set; } // Optional
    public int Amount { get; set; } = 0;
    public TransactionType ExpenseType { get; set; } = TransactionType.OneOff;
    public Timeframe? Timeframe { get; set; } // Optional
    public DateTime Date { get; set; } = DateTime.Now;
    public Category Category { get; set; } = Category.Other;
    public string UserId { get; set; } = "";
    public int ProjectId { get; set; } = 0;

    public Project? Project { get; set; }

  }


}