using SharedProject.Interfaces;
using SharedProject.Models;
using System.Web;

namespace WebClient.Services;

public class TaskService : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("https://localhost:54915/api/task");
    }

    public async Task<bool> Delete(string id)
    {
        var response = await _httpClient.DeleteAsync($"/delete/{HttpUtility.UrlEncode(id)}");

        var success = await response.Content.ReadFromJsonAsync<bool>();
        return success;
    }

    public async Task<IEnumerable<TaskItem>> GetAll()
    {
        var response = await _httpClient.GetAsync("/list");

        var items = await response.Content.ReadFromJsonAsync<IEnumerable<TaskItem>>();
        return items;
    }

    public async Task<bool> Save(TaskItem item)
    {
        var response = await _httpClient.PostAsJsonAsync<TaskItem>("/save", item);

        var success = await response.Content.ReadFromJsonAsync<bool>();
        return success;
    }
}
