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
        // TODO : CHECK IT OUT
        public async Task<OperationResult> CreateProject(ProjectApiModel model)
        {
            // Initialize Operation Result
            OperationResult result;
            try
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
                var projects = await _context.Projects.ToListAsync();
                result = new OperationResult
                {
                    Result = projects
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
        public async Task<OperationResult> AddTaskToProject(string taskId, string projectId)
        {
            // Initialize Operation Result
            OperationResult result;
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(c => c.Id == taskId);
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);

                if (task == null || project == null)
                {
                    result = new OperationResult
                    {
                        ErrorMessage = "Not found",
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else
                {
                    project.TaskId = taskId;

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
