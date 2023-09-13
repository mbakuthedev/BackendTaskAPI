using BackendTaskAPI.ApiModels;
using BackendTaskAPI.Result;

namespace BackendTaskAPI.BackendTaskAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult> Login(string userName, string password);
    }
}
