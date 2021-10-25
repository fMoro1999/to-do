using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/todos/")]
    public class ToDoController : ControllerBase
    {
        public readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService) => _toDoService = toDoService;

        [HttpGet]
        public ActionResult<IEnumerable<ToDo>> GetAll() => _toDoService.GetAll();

        [HttpGet("{id:length(24)}", Name = "GetToDo")]
        public ActionResult<ToDo> GetToDo([FromRoute] string id)
        {
            ToDo toDo = _toDoService.Get(id);

            if (toDo is null)
                return NotFound();

            return Ok(toDo);
        }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Create([FromBody] ToDo toDo)
        {
            if(toDo is null)
                return BadRequest();
            
            _toDoService.Add(toDo);
            return CreatedAtRoute(nameof(GetToDo), new { id = toDo.Id }, toDo);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update([FromRoute] string id, [FromBody] ToDo toDo)
        {
            ToDo existingtoDo = _toDoService.Get(id);

            if (existingtoDo is null)
                return NotFound();

            _toDoService.Update(id, toDo);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete([FromRoute] string id)
        {
            ToDo toDo = _toDoService.Get(id);

            if (toDo == null)
                return NotFound();

            _toDoService.Delete(id);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            List<ToDo> toDos = _toDoService.GetAll();

            toDos.ForEach(x => Delete(x.Id));

            return NoContent();
        }
    }
}