using BackendTaskAPI.ApiModels;
using BackendTaskAPI.Result;

namespace BackendTaskAPI.BackendTaskAPI.Application.Interfaces
{
    public interface IUserService
    {
        string Login(string userName, string password);
    }
}
