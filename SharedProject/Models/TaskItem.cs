namespace SharedProject.Models;

public class TaskItem
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool Done { get; set; }
}
