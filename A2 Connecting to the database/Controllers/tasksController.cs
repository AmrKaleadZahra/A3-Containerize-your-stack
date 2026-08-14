using A2_Connecting_to_the_database.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace A2_Connecting_to_the_database.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class tasksController : ControllerBase
        {

            private readonly ITaskService _taskService;
            
            public tasksController(ITaskService taskService)
            {
                _taskService = taskService;
            }

            [HttpGet]
            public ActionResult<ApiDetails> Get()
            {
                return
                    new ApiDetails
                    {
                        Name = "Task Api",
                        Version = "1.0",
                        Endpoint = { "/tasks" },
                    };
            }

            [HttpGet("health")]
            public ActionResult IsHealth()
            {
                return Ok(new
                {
                    status = "Ok"
                });
            }

            [HttpGet("tasks")]
            public async Task<ActionResult<List<TaskItem>>> GetTasks()
            {
                try
                {
                    return Ok(await _taskService.GetTasks());
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new
                    {
                        error = ex.Message
                    });
                }

        }

            [HttpGet("task/{id}")]
            public async Task<ActionResult<TaskItem>> GetTaskById(int id)
            {
                try
                {   
                    return Ok(await _taskService.GetTaskById(id));
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new
                    {
                        error = ex.Message
                    });
                }


            }

            [HttpGet("task")]
            public async Task<ActionResult<List<TaskItem>>> GetTasks([FromQuery] bool? done, [FromQuery] string? search)
            {
                
                try
                {
                    return Ok(await _taskService.IsDoneSearch(done, search));
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new
                    {
                        error = ex.Message
                    });
                }
            
            }


        [HttpGet("task/stats")]
        public async Task<ActionResult<TaskStats>> GetTaskStats()
        {
            try
            {
                TaskStats stats = new TaskStats
                {
                    TotalTasks = await _taskService.tasksCount(),
                    CompletedTasks = await _taskService.DoneCount(),
                    PendingTasks = await _taskService.PendingCount()
                };
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message
                });

            }
        }
            [HttpPost("task/reset")]
            public async Task<ActionResult> ResetTasks()
            {
                try
                {
                    await _taskService.Reset();
                    return Created();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        error = ex.Message
                    });
                }
            }

            [HttpPost("task")]
            public async Task<ActionResult<TaskItem>> Createtask([FromBody] CreateTaskRequest request)
            {
                try
                {
                    var newTask = await _taskService.CreateTask(request);

                    return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);    
                }
                catch (ArgumentNullException ex)
                {
                    return BadRequest(new
                    {
                        error = ex.Message
                    });
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        error = ex.Message
                    });
                }
            }

            [HttpPut("task/{id}")]
            public async Task<ActionResult<TaskItem>> UpdateTask(int id, [FromBody] UpdatedTask updated)
            {
                try
                {
                    return await _taskService.UpdateTask(id, updated) ? Ok(updated) : NotFound(new
                    {
                        error = $"Task {id} not found"
                    });
                }
            catch (ArgumentNullException ex)
                {
                    return BadRequest(new
                    {
                        error = ex.Message
                    });
                
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new
                    {
                        error = ex.Message
                    });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        error = ex.Message
                    });
                }
            }

            [HttpDelete("task/{id}")]
            public async Task<ActionResult> DeleteTask(int id)
            {
                try
                {
                    return await _taskService.DeleteTaskById(id) ?
                    Ok(new
                    {
                        message = $"Task {id} deleted successfully"
                    }) : BadRequest(new
                    {
                        error = $"Task {id} can not be deleted"
                    });
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(new
                    {
                        error = ex.Message
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        error = ex.Message
                    });
                }
            }
        }

}

