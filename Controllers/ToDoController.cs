using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Interfaces;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IToDoServices todoServices;

        public TodoController(IToDoServices todoServices)
        {
            this.todoServices = todoServices;
        }



        // Creating new Todo Item
        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {

                await todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Item added to TODO List successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the Todo Item", error = ex.Message });

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateToDoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = await todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = "Todo item updated successfully", data = new { todo.Id, todo.Title } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the Todo item", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                var isDeleted = await todoServices.DeleteTodoAsync(id);
                if (!isDeleted)
                {
                    return NotFound(new { message = "Todo item not found" });
                }
                return Ok(new { message = "Todo item deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the Todo item", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await todoServices.GetByIdAsync(id);
                return Ok(new { message = "Successfully retrieved the Todo item", data = todo.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the Todo item", error = ex.Message });
            }
        }

        // Get all Todo Items

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await this.todoServices.GetAllAsync();
                if (todo == null || !todo.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }
                return Ok(new { message = "Successfully retrieved all TODO items", data = todo });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Tood it posts", error = ex.Message });


            }
        }

    }
}