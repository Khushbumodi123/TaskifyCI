using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DueDate { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}