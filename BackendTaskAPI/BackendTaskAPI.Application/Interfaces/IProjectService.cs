using BackendTaskAPI.ApiModels;
using BackendTaskAPI.Result;

namespace BackendTaskAPI.BackendTaskAPI.Application.Interfaces
{
    public interface IProjectService
    {
        Task<OperationResult> CreateProject(ProjectApiModel model);
        Task<OperationResult> GetProjects();
        Task<OperationResult> AddTaskToProject(string taskId, string projectId);
        Task<OperationResult> ModifyProject(string id, ProjectApiModel model);
        Task<OperationResult> DeleteProject(string id);
    }
}
