using Microsoft.AspNetCore.Components;
using SharedProject.Models;

namespace WebClient.Components.Shared;

public partial class TaskComponent
{
    [Parameter] public TaskItem Value { get; set; }
    [Parameter] public EventCallback<TaskItem> ValueChanged { get; set; }

    private async void OnCheckChanged(bool value)
    {
        Value.Done = value;

        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(Value);
    }
}
