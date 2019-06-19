using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.api.Filters;
using Todo.api.Models;
using Todo.api.Specifications;
using Todo.api.Logger;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        private readonly ToDoUnitOfWork _toDoUnitOfWork;
        
        private readonly ICustomLogger _logger;

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

            _toDoUnitOfWork = new ToDoUnitOfWork(context);
            _logger = CustomLoggerFactory.GetLogger();
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            _logger.LogInfo("get all to do items api called.");
            return await _toDoUnitOfWork.ToDoRepository.ListAllAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _toDoUnitOfWork.ToDoRepository.GetByIdAsync(id);

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
            await _toDoUnitOfWork.ToDoRepository.AddAsync(todoItem);
            _toDoUnitOfWork.SaveChanges();
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

            await _toDoUnitOfWork.ToDoRepository.UpdateAsync(todoItem);
            _toDoUnitOfWork.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            var todoItem = await _toDoUnitOfWork.ToDoRepository.GetByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _toDoUnitOfWork.ToDoRepository.DeleteAsync(todoItem);
            _toDoUnitOfWork.SaveChanges();
            return todoItem;
        }

        //  GET: api/Todo/GetTodoItemStartWith?searchString=Create
        [Route("GetTodoItemStartWith")]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItemStartWith(string searchString)
        {
            var startWithSpec = new TodoSpecification(searchString, 0, 10);
            return await _toDoUnitOfWork.ToDoRepository.ListAsync(startWithSpec);
        }

        // only work for SQL, in memory is no-sql database
        [Route("GetCompletedTodoItems")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetCompletedTodoItems()
        {
            var rawSql = $"select * from TodoList where IsComplted={0}";
            return await _toDoUnitOfWork.ToDoRepository.FromSqlAsync(rawSql, 1);
        }
    }
}