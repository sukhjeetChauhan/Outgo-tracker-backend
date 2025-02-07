namespace Outgo_tracker_Backend.Models
{
  public class ProjectJoinRequest
  {
    public int Id { get; set; } = 0;
    public int ProjectId { get; set; } = 0;
    public string UserId { get; set; } = "";
    public string UserName { get; set; } = "";
    public Status Status { get; set; } = Status.Pending;
  }
}