using BackendTaskAPI.ApiModels;
using BackendTaskAPI.Data;
using BackendTaskAPI.DataModels;
using BackendTaskAPI.Result;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;

namespace BackendTaskAPI.Models
{
    public class UserOperations
    {
    
            private readonly ApplicationDbContext _context;
            private readonly ILogger<UserOperations> _logger;
            public UserOperations(ApplicationDbContext context, ILogger<UserOperations> logger)
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

        }
    }

