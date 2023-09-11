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

        public async Task<OperationResult> GetProjects()
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
            return result;
        }
        public async Task<OperationResult> AddTaskToProject(string taskId, ProjectDataModel project)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
                var tasks = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == taskId);
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
                    project.Tasks = ;
                     _context.Projects.Update(project);
                    result = new OperationResult
                    {
                        Result = project
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

        public async Task<OperationResult> ModifyProject(string id, ProjectApiModel model)
        {
            OperationResult result;
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    model.ProjectDescription = project.ProjectDescription;
                    model.ProjectName = project.ProjectName;

                    _context.Projects.Update(project);

                    await _context.SaveChangesAsync();
                    result = new OperationResult
                    {
                        Result = project
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
        public async Task<OperationResult> DeleteProject(string id)
        {
            // Initialize operation result
            OperationResult result;
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Task does not exist",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    if (project.Tasks != null)
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            var entity = context.Projects.Find(id);
                            context.Projects.Remove(entity);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        _context.Projects.Remove(project);
                       
                    }
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

      
    }
}
