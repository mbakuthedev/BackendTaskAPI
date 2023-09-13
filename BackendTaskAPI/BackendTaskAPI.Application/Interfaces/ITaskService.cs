using BackendTaskAPI.ApiModels;
using BackendTaskAPI.DataModels;
using BackendTaskAPI.Models;
using BackendTaskAPI.Result;

namespace BackendTaskAPI.BackendTaskAPI.Application.Interfaces
{
    public interface ITaskService
    {
        Task<OperationResult> CreateTasks(TaskApiModel model);
        Task<IEnumerable<TaskDataModel>> GetTasks();
        Task<OperationResult> FetchTasksByPriority(Priority Taskpriority, TaskApiModel model);
        Task<OperationResult> FetchTasksByStatus(Status TaskStatus, TaskApiModel model);
        Task<OperationResult> ModifyTasks(string id, TaskApiModel model);
        Task<OperationResult> DeleteTasks(string id);
        Task<OperationResult> FetchTasksByDueDate(DateTime dueDate);
    }
}
