using SharedProject.Models;

namespace SharedProject.Interfaces;

public interface ITaskService
{
    Task<bool> Delete(string id);
    Task<IEnumerable<TaskItem>> GetAll();
    Task<bool> Save(TaskItem item);
}