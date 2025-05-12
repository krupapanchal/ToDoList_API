using Microsoft.EntityFrameworkCore;
using ToDoList_API.Models;

namespace ToDoList_API.DAL
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ToDoDbContext _context;
         
        public TodoRepository(ToDoDbContext context)
        {
            _context = context;
        }
        public async Task<Todo> AddAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
