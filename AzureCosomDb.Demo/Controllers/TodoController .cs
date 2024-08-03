using AzureCosomDb.Demo.Models;
using AzureCosomDb.Demo.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace AzureCosomDb.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public TodoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET: api/GetToDos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetToDos()
        {
            try
            {
                var todos = await _toDoService.GetToDoDetails();
                return Ok(todos);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/GetToDoById/"5"
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetToDoById(string id,string partitionKey)
        {
            try
            {
               
                var todo = await _toDoService.GetToDoDetailsById(id, partitionKey);
                if (todo == null)
                {
                    return NotFound();
                }
                return Ok(todo);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/CreateToDo
        [HttpPost]
        public async Task<IActionResult> CreateToDo([FromBody] TodoItem item)
        {
            try
            {
                await _toDoService.CreateToDo(item);
                return CreatedAtAction("CreateToDo", new { id = item.id }, item);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/DeleteTodo/"5"
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(string id, string partitionKey)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(partitionKey))
            {
                return BadRequest("Invalid ID or partition key.");
            }


            try
            {
                await _toDoService.DeleteToDo(id,  partitionKey);
                return NoContent();
            }
            
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
