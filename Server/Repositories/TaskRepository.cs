using Microsoft.EntityFrameworkCore;
using Server.Interfaces;
using SharedProject.Models;

namespace Server.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly Context _context;

    public TaskRepository(Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        var items = await _context.Tasks.ToListAsync();
        return items;
    }

    public async Task<bool> Save(TaskItem item)
    {
        try
        {
            bool newItem = false;
            if (string.IsNullOrWhiteSpace(item.Id))
            {
                item.Id = Guid.NewGuid().ToString();
                newItem = true;
            }

            TaskItem? existingItem = null!;

            if (newItem == false)
                existingItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (existingItem == null)
            {
                var entity = await _context.Tasks.AddAsync(item);
            }
            else
            {
                existingItem.Title = item.Title;
                existingItem.Description = item.Description;
                existingItem.ExpireDate = item.ExpireDate;
                existingItem.Done = item.Done;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Delete(string id)
    {
        var existingItem = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingItem != null)
        {
            _context.Tasks.Remove(existingItem);
            await _context.SaveChangesAsync();
        }

        return existingItem != null;
    }
}
