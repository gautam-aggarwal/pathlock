using System.ComponentModel.DataAnnotations;

namespace ProjectManager.API.Models {
  public class TaskItem {
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
  }
}
