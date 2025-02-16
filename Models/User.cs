namespace Outgo_tracker_Backend.Models

{
  public class User
  {

    public string Id { get; set; } = default!;
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public int PhoneNumber { get; set; } = default!;
    public int DefaultProjectId { get; set; } = default!;
  }
}