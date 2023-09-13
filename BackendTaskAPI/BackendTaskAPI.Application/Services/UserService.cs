using BackendTaskAPI.ApiModels;
using BackendTaskAPI.BackendTaskAPI.Application.Interfaces;
using BackendTaskAPI.BackendTaskAPI.Domain.Models;
using BackendTaskAPI.Data;
using BackendTaskAPI.Domain.DataModels;
using BackendTaskAPI.Extensions;
using BackendTaskAPI.Result;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace BackendTaskAPI.Models
{
    public class UserService : IUserService
    {
    
            private readonly ApplicationDbContext _context;
            private readonly ILogger<UserService> _logger;
            public UserService(ApplicationDbContext context, ILogger<UserService> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<OperationResult> RegisterUser(UserApiModel model)
            {
                // Initialize Operation Result
                OperationResult result;
                try
                {
                    var newUser = await _context.AddAsync(new UserDataModel
                    {
                       FirstName = model.FirstName,
                       LastName = model.LastName,
                       Email = model.Email,
                       
                    });

                    _context.SaveChanges();

                    result = new OperationResult
                    {
                        Result = newUser
                    };
                }
                catch (Exception ex)
                {
                    // Log the error 
                    _logger.LogError("An error occurred. Details: {error}", ex.Message);

                    result = new OperationResult
                    {
                        ErrorTitle = "SYSTEM ERROR",
                        ErrorMessage = "Transaction could not be initiated",
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
                return result;
            }
            public async Task<OperationResult> ModifyUser(string id, UserApiModel model)
            {
            // Initialize Operation result
            OperationResult result;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "User not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    // update user
                    _context.Users.Update(user);

                    // save changes
                    await _context.SaveChangesAsync();

                    result = new OperationResult
                    {
                        Result = user
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the error 
                _logger.LogError("An error occurred. Details: {error}", ex.Message);

                result = new OperationResult
                {
                    ErrorTitle = "SYSTEM ERROR",
                    ErrorMessage = "Transaction could not be initiated",
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            return result;
            }
            public async Task<OperationResult> GetUser(string id)
            {
                // Initialize operation result
                OperationResult result;
                try
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Users not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                
                    result = new OperationResult
                    {
                        Result = new { user }
                    };         
                  
                }
                catch (Exception ex)
                {
                    // Log the error 
                    _logger.LogError("An error occurred. Details: {error}", ex.Message);

                    result = new OperationResult
                    {
                        ErrorTitle = "SYSTEM ERROR",
                        ErrorMessage = "Transaction could not be initiated",
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
                return result;
            }
            public async Task<OperationResult> DeleteUser(string id)
            {
                // Initialize Operation Result
                OperationResult result;
                try
                {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "User does not exist",
                        StatusCode = (int)HttpStatusCode.NotFound

                    };
                }
                else
                {
                    _context.Users.Remove(user);
                    result = new OperationResult
                    {
                        Result = new { Message = "Record deleted " }
                    };
                }
                }
                catch (Exception ex)
                {
                    // Log the error 
                    _logger.LogError("An error occurred. Details: {error}", ex.Message);

                    result = new OperationResult
                    {
                        ErrorTitle = "SYSTEM ERROR",
                        ErrorMessage = "Transaction could not be initiated",
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
            }
            return result;
        }

        public string Login(string userName, string password)
        {
                var user = _context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

                // return null if user not found
                if (user == null)
                {
                    return string.Empty;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(ApplicationExtension.SECRET);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                    }),

                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                return user.Token;
            }
    }
    }

