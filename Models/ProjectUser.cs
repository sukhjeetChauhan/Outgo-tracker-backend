namespace Outgo_tracker_Backend.Models
{


  public class ProjectUser
  {
    public int Id { get; set; } // Primary key

    public int ProjectId { get; set; }
    public string UserId { get; set; } = string.Empty;

    // Navigation property (optional but recommended for bidirectionality)
    public Project? Project { get; set; }
  }
}