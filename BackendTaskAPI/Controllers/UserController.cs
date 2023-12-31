﻿using BackendTaskAPI.ApiModels;
using BackendTaskAPI.BackendTaskAPI.Application.Interfaces;
using BackendTaskAPI.EndpointRoutes;
using BackendTaskAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTaskAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Scoped instance of task operation
        /// </summary>
        private readonly UserService _operation;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="operation"></param>
        public UserController(UserService operation)
        {
            _operation = operation;
        }

        [AllowAnonymous]
        [HttpPost(EndpointRoute.Login)]
        public async Task<ActionResult> Login([FromBody] UserApiModel user)
        {
            var token = await _operation.Login(user.UserName, user.Password);

            if (!token.Successful)
            {
                return Problem(detail:token.ErrorMessage);
            }

            return Ok(token);
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
