using BackendTaskAPI.ApiModels;
using BackendTaskAPI.EndpointRoutes;
using BackendTaskAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTaskAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Scoped instance of task operation
        /// </summary>
        private readonly UserOperations _operation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operation"></param>
        public UserController(UserOperations operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// An endpoint to register a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(EndpointRoute.RegisterUser)]
        public async Task<ActionResult> RegisterUser(UserApiModel model)
        {
            var operation = await _operation.RegisterUser(model);
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
        [HttpGet(EndpointRoute.FetchUser)]
        public async Task<ActionResult<IEnumerable<UserApiModel>>> GetUser(string id)
        {
            var user = await _operation.GetUser(id);
            return Ok(user);
        }

        /// <summary>
        /// An endpoint to modify user information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut(EndpointRoute.ModifyUser)]
        public async Task<ActionResult> ModifyUser(string id, UserApiModel model)
        {
            var user = await _operation.ModifyUser(id, model);
            if (!user.Successful)
            {
                return Problem(
                    detail: user.ErrorMessage,
                    statusCode: user.StatusCode
                    );
            }
            return Created(string.Empty, user);
        }

        /// <summary>
        /// An endpoint to delete projects
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete(EndpointRoute.DeleteUser)]
        public async Task<ActionResult> DeleteProject(string id)
        {
            await _operation.DeleteUser(id);
            return NoContent();
        }
    }
}
