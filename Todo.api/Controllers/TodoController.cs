using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.api.Models;
using Todo.api.Repositories;
using Todo.api.Specifications;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        private readonly TodoRepository _repository;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.TodoItems.Add(new TodoItem { Id = 1, Name = "Create design", IsComplete = true });
                _context.TodoItems.Add(new TodoItem { Id = 2, Name = "Create web apis", IsComplete = true });
                _context.TodoItems.Add(new TodoItem { Id = 3, Name = "ingrate deisgn and webapis", IsComplete = false });
                _context.TodoItems.Add(new TodoItem { Id = 4, Name = "deploy software", IsComplete = false });
                _context.SaveChanges();
            }

            _repository = new TodoRepository(_context);
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _repository.ListAllAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _repository.AddAsync(todoItem);

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(todoItem);

            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(todoItem);

            return todoItem;
        }

        //  GET: api/Todo/GetTodoItemStartWith?searchString=Create
        [Route("GetTodoItemStartWith")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemStartWith(string searchString)
        {
            var startWithSpec = new TodoSpecification(b => b.Name.StartsWith(searchString));
            return await _repository.ListAsync(startWithSpec);
        }
    }
}