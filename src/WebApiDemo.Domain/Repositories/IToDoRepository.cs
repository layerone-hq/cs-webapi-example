using WebApiDemo.Domain.Entities;

namespace WebApiDemo.Domain.Repositories;

public interface IToDoRepository
{
    Task<IEnumerable<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task<ToDoItem> AddAsync(ToDoItem item);
    Task<ToDoItem?> UpdateAsync(int id, ToDoItem updatedItem);
    Task<ToDoItem?> DeleteAsync(int id);
}
