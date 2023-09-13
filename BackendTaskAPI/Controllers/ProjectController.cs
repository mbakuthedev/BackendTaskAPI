using BackendTaskAPI.ApiModels;
using BackendTaskAPI.EndpointRoutes;
using BackendTaskAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendTaskAPI.Controllers
{

    [ApiController]
    public class ProjectController : ControllerBase
    {
        /// <summary>
        /// Scoped instance of task operation
        /// </summary>
        private readonly ProjectService _operation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operation"></param>
        public ProjectController(ProjectService operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// An endpoint to create projects
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(EndpointRoute.CreateProject)]
        public async Task<ActionResult> CreateProject(ProjectApiModel model)
        {
            var operation = await _operation.CreateProject(model);
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
        /// An endpoint to fetch all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet(EndpointRoute.FetchProject)]
        public async Task<ActionResult<IEnumerable<ProjectApiModel>>> GetProjects()
        {
            var tasks = await _operation.GetProjects();
            return Ok(tasks);
        }

        [HttpPatch(EndpointRoute.AddTaskToProject)]
        public async Task<ActionResult> AddTaskToProject(string taskId, string projectId)
        {
            var operation = await _operation.AddTaskToProject(taskId,projectId);
            if (!operation.Successful)
            {
                return Problem(
                    detail: operation.ErrorMessage,
                    statusCode: operation.StatusCode
                    );
            }
            return Created(string.Empty, operation);
        }

        /// <summary>
        /// An endpoint to modify projects
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch(EndpointRoute.ModifyProject)]
        public async Task<ActionResult> ModifyProject(string id, ProjectApiModel model)
        {
            var tasks = await _operation.ModifyProject(id, model);
            if (!tasks.Successful)
            {
                return Problem(
                    detail: tasks.ErrorMessage,
                    statusCode: tasks.StatusCode
                    );
            }
            return Created(string.Empty, tasks);
        }

        /// <summary>
        /// An endpoint to delete projects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(EndpointRoute.DeleteProject)]
        public async Task<ActionResult> DeleteProject(string id)
        {
            await _operation.DeleteProject(id);
            return NoContent();
        }
    }
}
