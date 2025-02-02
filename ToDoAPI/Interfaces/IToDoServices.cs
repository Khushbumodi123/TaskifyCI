using ToDoAPI.Models;

namespace ToDoAPI.Interfaces
{
    public interface IToDoServices
    {
        Task<Todo> CreateTodoAsync(CreateTodoRequest request);
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task<Todo> UpdateTodoAsync(Guid id, UpdateTodoRequest request);
        Task<bool> DeleteTodoAsync(Guid id);
    }

}