using BackendTaskAPI.EndpointRoutes;
using BackendTaskAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTaskAPI.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// Scoped instance of task operation
        /// </summary>
        private readonly TaskService _operation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operation"></param>
        public TaskController(TaskService operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// An endpoint to create tasks
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(EndpointRoute.CreateTask)]
        public async Task<ActionResult> CreateTask(TaskApiModel model)
        {
            var operation = await _operation.CreateTasks(model);
            if (!operation.Successful)
            {
                return Problem(
                    detail: operation.ErrorMessage,
                    statusCode: operation.StatusCode
                     );
            }
            return Created(string.Empty, model);
        }

        /// <summary>
        /// An endpoint to fetch all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet(EndpointRoute.FetchTasks)]
        public async Task<ActionResult<IEnumerable<TaskApiModel>>> GetTasks()
        {
            var tasks = await _operation.GetTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// An endpoint to get tasks by their priorities
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet(EndpointRoute.FetchTaskByPriority)]
        public async Task<ActionResult> GetTasksByPriority(Priority priority, TaskApiModel model)
        {
            var tasks = await _operation.FetchTasksByPriority(priority, model);
            if (!tasks.Successful)
            {
                return Problem(
                    detail: tasks.ErrorMessage,
                    statusCode: tasks.StatusCode
                    );
            }
            return Ok(tasks);
        }

        /// <summary>
        /// An endpoint to get tasks by status
        /// </summary>
        /// <param name="status"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet(EndpointRoute.FetchTaskByStatus)]
        public async Task<ActionResult> GetTasksByStatus(Status status, TaskApiModel model)
        {
            var tasks = await _operation.FetchTasksByStatus(status, model);
            if (!tasks.Successful)
            {
                return Problem(
                    detail: tasks.ErrorMessage,
                    statusCode: tasks.StatusCode
                    );
            }
            return Ok(tasks);
        }

        /// <summary>
        /// An endpoint to modify tasks
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut(EndpointRoute.ModifyTask)]
        public async Task<ActionResult> ModifyTasks(string id, TaskApiModel model)
        {
            var tasks = await _operation.ModifyTasks(id, model);
            if (!tasks.Successful)
            {
                return Problem(
                    detail: tasks.ErrorMessage,
                    statusCode: tasks.StatusCode
                    );
            }
            return Created(string.Empty,tasks);
        }

        /// <summary>
        /// An endpoint to delete tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(EndpointRoute.DeleteTask)]
        public async Task<ActionResult> DeleteTask(string id)
        {
            await _operation.DeleteTasks(id);
            return NoContent();
        }
    }
}

