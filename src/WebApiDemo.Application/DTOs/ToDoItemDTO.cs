using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Application.DTOs;

public class ToDoItemDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters")]
    public string Title { get; set; } = null!;

    public bool IsComplete { get; set; }
}
