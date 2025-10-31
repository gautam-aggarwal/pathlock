namespace ProjectManager.API.DTOs {
  public class TaskDto {
    public string Title { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
  }
}
