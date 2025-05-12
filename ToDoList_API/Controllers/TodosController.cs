using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.DAL;
using ToDoList_API.Models;

namespace ToDoList_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodosController(ITodoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            var todos = await _repository.GetAllAsync();
            return Ok(todos);
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
        {
            var created = await _repository.AddAsync(todo);
            return CreatedAtAction(nameof(GetTodos), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, Todo updatedTodo)
        {
            if (id != updatedTodo.Id) return BadRequest();

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Title = updatedTodo.Title;
            existing.Description = updatedTodo.Description;
            existing.StartDate = updatedTodo.StartDate;
            existing.EndDate = updatedTodo.EndDate;
            existing.IsCompleted = updatedTodo.IsCompleted;

            await _repository.UpdateAsync(existing);
            return NoContent();
        }
    }
}
