using Microsoft.AspNetCore.JsonPatch;
using WebApiDemo.Domain.Entities;

namespace WebApiDemo.Application.Interfaces;

public interface IToDoService
{
    Task<IEnumerable<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<ToDoItem> AddAsync(ToDoItem newItem);
    Task<ToDoItem?> UpdateAsync(int id, ToDoItem updatedItem);
    Task PatchAsync(int id, JsonPatchDocument<ToDoItem> patchDoc);
    Task<ToDoItem?> DeleteAsync(int id);
}
