using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.AppDataContext;
using ToDoAPI.Models;
using ToDoAPI.Interfaces;

namespace ToDoAPI.Services
{
    public class ToDoServices : IToDoServices
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<ToDoServices> _logger;
        private readonly IMapper _mapper;

        public ToDoServices(TodoDbContext context, ILogger<ToDoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }




        //  Create Todo for it be save in the datbase 

        public async Task<Todo> CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.Now;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
                return todo; // Return the created Todo item
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the todo item.");
                throw new Exception("An error occurred while creating the todo item.");
            }
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new Exception($"Todo item with ID {id} not found.");
            }
            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new Exception($"Todo item with ID {id} not found.");
            }
            _mapper.Map(request, todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<bool> DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                throw new Exception($"Todo item with ID {id} not found.");
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            try
            {
                var todos = await _context.Todos.ToListAsync();
                if (todos == null || !todos.Any())
                {
                    throw new Exception("No Todo items found");
                }
                return todos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all Todo items.");
                throw new Exception("An error occurred while retrieving all Todo items.", ex);
            }
        }

    }
}