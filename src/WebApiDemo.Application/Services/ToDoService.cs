using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using WebApiDemo.Application.Interfaces;
using WebApiDemo.Domain.Entities;
using WebApiDemo.Domain.Repositories;

namespace WebApiDemo.Application.Services;

public class ToDoService : IToDoService
{
    private readonly IToDoRepository _repository;
    private readonly ILogger<ToDoService> _logger;

    public ToDoService(IToDoRepository repository, ILogger<ToDoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public Task<IEnumerable<ToDoItem>> GetAllAsync() => _repository.GetAllAsync();

    public Task<ToDoItem?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<ToDoItem> AddAsync(ToDoItem newItem)
    {
        var item = await _repository.AddAsync(newItem);
        _logger.LogInformation("Created ToDoItem {Id}", item.Id);
        return item;
    }

    public async Task<ToDoItem?> UpdateAsync(int id, ToDoItem updatedItem)
    {
        var item = await _repository.UpdateAsync(id, updatedItem);
        if (item == null)
            _logger.LogWarning("UpdateAsync: ToDoItem {Id} not found", id);
        else
            _logger.LogInformation("Updated ToDoItem {Id}", id);
        return item;
    }

    public async Task PatchAsync(int id, JsonPatchDocument<ToDoItem> patchDoc)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogWarning("PatchAsync: ToDoItem {Id} not found", id);
            return;
        }
        patchDoc.ApplyTo(item);
        await _repository.UpdateAsync(id, item);
        _logger.LogInformation("Patched ToDoItem {Id}", id);
    }

    public async Task<ToDoItem?> DeleteAsync(int id)
    {
        var item = await _repository.DeleteAsync(id);
        if (item == null)
            _logger.LogWarning("DeleteAsync: ToDoItem {Id} not found", id);
        else
            _logger.LogInformation("Deleted ToDoItem {Id}", id);
        return item;
    }
}
