using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Application.DTOs;
using WebApiDemo.Application.Interfaces;
using WebApiDemo.Domain.Entities;

namespace WebApiDemo.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IToDoService _toDoService;

    public TodoController(IToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _toDoService.GetAllAsync();
        if (!items.Any()) return NoContent();
        return Ok(items.Select(MapToDTO).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetToDoItemById(int id)
    {
        var item = await _toDoService.GetByIdAsync(id);
        return item == null ? NotFound() : Ok(MapToDTO(item));
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItemDTO newItemDto)
    {
        if (newItemDto == null) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var item = await _toDoService.AddAsync(new ToDoItem
        {
            Title = newItemDto.Title,
            IsComplete = newItemDto.IsComplete
        });
        return CreatedAtAction(nameof(GetToDoItemById), new { id = item.Id }, MapToDTO(item));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoItem(int id, [FromBody] ToDoItemDTO updateItemDto)
    {
        if (updateItemDto == null) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var item = await _toDoService.UpdateAsync(id, new ToDoItem
        {
            Title = updateItemDto.Title,
            IsComplete = updateItemDto.IsComplete
        });
        return item == null ? NotFound() : Ok(MapToDTO(item));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchToDoItem(int id, [FromBody] JsonPatchDocument<ToDoItem> patchDoc)
    {
        await _toDoService.PatchAsync(id, patchDoc);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(int id)
    {
        var item = await _toDoService.DeleteAsync(id);
        return item == null ? NotFound() : Ok(MapToDTO(item));
    }

    private static ToDoItemDTO MapToDTO(ToDoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        IsComplete = item.IsComplete
    };
}
