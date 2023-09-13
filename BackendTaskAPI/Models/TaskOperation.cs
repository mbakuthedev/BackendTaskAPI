using BackendTaskAPI.Data;
using BackendTaskAPI.DataModels;
using BackendTaskAPI.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;

namespace BackendTaskAPI.Models
{
    public class TaskOperation
    {
        private readonly ApplicationDbContext   _context;
        private readonly ILogger<TaskOperation> _logger;
        public TaskOperation(ApplicationDbContext context, ILogger<TaskOperation> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> CreateTasks(TaskApiModel model)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
                var Usertasks = await _context.AddAsync(new TaskDataModel
                {
                    Description = model.Description,
                    Title = model.Title,
                    Status = model.Status,
                    DueDate = model.DueDate,
                    Priority = model.Priority
                });

                _context.SaveChanges();

                result = new OperationResult
                {
                    Result = Usertasks
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

        public async Task<IEnumerable<TaskDataModel>> GetTasks()
        {
            // Initialize operation result
            OperationResult result;
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                result = new OperationResult
                {
                    Result = tasks
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
            return (IEnumerable<TaskDataModel>)result;
        }
        public async Task<OperationResult> FetchTasksByPriority(Priority Taskpriority, TaskApiModel model)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
               var tasksBasedOnPriority =  await _context.Tasks.Where(x => x.Priority == Taskpriority).ToListAsync();
                result = new OperationResult
                {
                    Result = tasksBasedOnPriority
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

        public async Task<OperationResult> FetchTasksByStatus(Status TaskStatus, TaskApiModel model)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
                var tasksBasedOnStatus = await _context.Tasks.Where(x => x.Status == TaskStatus).ToListAsync();
                result = new OperationResult
                {
                    Result = tasksBasedOnStatus
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

        public async Task<OperationResult> ModifyTasks(string id, TaskApiModel model)
        {
            OperationResult result;
            try
            {
               var task =  await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                if (task == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    model.Priority = task.Priority;
                    model.Status = task.Status;
                    model.Title = task.Title;
                    model.DueDate = task.DueDate;

                    _context.Tasks.Update(task);

                   await _context.SaveChangesAsync();
                    result = new OperationResult
                    {
                        Result = task
                    };
                }
            }
            catch (Exception  ex)
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
        public async Task<OperationResult> DeleteTasks(string id)
        {
            // Initialize operation result
            OperationResult result;
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
                if (task == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task does not exist",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    _context.Tasks.Remove(task);
                    result = new OperationResult
                    {
                        Result = new { Message = "Removed Sucessfully" }
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

        public async Task<OperationResult> FetchTasksByDueDate(DateTime dueDate)
        {
            //
            OperationResult result;
            try
            {
               var tasksByDate = await  _context.Tasks.FirstOrDefaultAsync(x => x.DueDate.Date == dueDate.Date);
                if (tasksByDate == null)
                {
                    result = new OperationResult
                    {
                        Result = new { Message = "Task not found" }
                    };
                }
                else
                {
                    result = new OperationResult
                    {
                        Result = tasksByDate
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
