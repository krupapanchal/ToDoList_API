using ToDoList_API.Models;

namespace ToDoList_API.DAL
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<Todo> AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
    }
}
