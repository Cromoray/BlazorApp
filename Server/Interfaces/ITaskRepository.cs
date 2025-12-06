using SharedProject.Models;

namespace Server.Interfaces;

public interface ITaskRepository
{
    Task<bool> Delete(string id);
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<bool> Save(TaskItem item);
}