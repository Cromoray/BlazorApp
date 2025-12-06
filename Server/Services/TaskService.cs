using Server.Interfaces;
using SharedProject.Interfaces;
using SharedProject.Models;

namespace Server.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return items;
    }

    public async Task<bool> Save(TaskItem item)
    {
        if (item == null)
            return false;

        if (string.IsNullOrWhiteSpace(item.Title))
            return false;

        if (string.IsNullOrWhiteSpace(item.Description))
            return false;

        if (item.ExpireDate == null || item.ExpireDate.HasValue == false)
            return false;

        var result = await _repository.Save(item);
        return result;
    }

    public async Task<bool> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return false;

        var result = await _repository.Delete(id);
        return result;
    }
}
