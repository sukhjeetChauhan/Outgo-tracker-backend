namespace Outgo_tracker_Backend.Models
{
  public enum Timeframe
  {
    Monthly,
    Weekly,
    Quarterly,
    HalfYearly,
    Yearly
  }

  public enum Category
  {
    Food,
    Groceries,
    car,
    Business,
    Rent,
    Bills,
    Entertainment,
    Shopping,
    Health,
    Transport,
    Other
  }

  public enum TransactionType
  {
    OneOff,
    Recurring
  }

}
