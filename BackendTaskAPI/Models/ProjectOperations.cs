using BackendTaskAPI.ApiModels;
using BackendTaskAPI.Data;
using BackendTaskAPI.DataModels;
using BackendTaskAPI.Result;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BackendTaskAPI.Models
{
    public class ProjectOperation
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProjectOperation> _logger;
        public ProjectOperation(ApplicationDbContext context, ILogger<ProjectOperation> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<OperationResult> CreateProject(string id,ProjectApiModel model)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
               var tasks =  await _context.Tasks.FindAsync(id);
                if (tasks == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {

                    var Usertasks = await _context.AddAsync(new ProjectDataModel
                    {
                        ProjectDescription = model.ProjectDescription,
                        ProjectName = model.ProjectName,
      
                });

                    _context.SaveChanges();

                    result = new OperationResult
                    {
                        Result = Usertasks
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

        public IEnumerable<ProjectDataModel> GetProjects()
        {
            // Initialize operation result
            OperationResult result;
            try
            {
                var tasks = _context.Tasks.ToListAsync();
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
            return result;
        }
        public async Task<OperationResult> AddTaskToProject(TaskApiModel model, ProjectApiModel project)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
                var tasks = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == model.Id);
                if (tasks == null )
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    project.Tasks = model;
                     _context.Projects.Update(project);
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
                var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
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
                var tasksByDate = _context.Tasks.FirstOrDefault(x => x.DueDate.Date == dueDate.Date);

                result = new OperationResult
                {
                    Result = tasksByDate
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
    }
}
