using Microsoft.EntityFrameworkCore;
using WebApiDemo.Domain.Entities;
using WebApiDemo.Domain.Repositories;
using WebApiDemo.Infrastructure.Persistence;

namespace WebApiDemo.Infrastructure.Repositories;

public class ToDoRepository : IToDoRepository
{
    private readonly ToDoDbContext _context;

    public ToDoRepository(ToDoDbContext context) => _context = context;

    public async Task<IEnumerable<ToDoItem>> GetAllAsync() =>
        await _context.ToDoItems.AsNoTracking().ToListAsync();

    public Task<ToDoItem?> GetByIdAsync(int id) =>
        _context.ToDoItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<ToDoItem> AddAsync(ToDoItem item)
    {
        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ToDoItem?> UpdateAsync(int id, ToDoItem updatedItem)
    {
        var existing = await _context.ToDoItems.FindAsync(id);
        if (existing == null) return null;
        existing.Title = updatedItem.Title;
        existing.IsComplete = updatedItem.IsComplete;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<ToDoItem?> DeleteAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return null;
        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
        return item;
    }
}
