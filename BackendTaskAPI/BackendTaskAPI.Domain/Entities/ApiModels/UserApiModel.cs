using BackendTaskAPI.Models;

namespace BackendTaskAPI.ApiModels
{
    public class UserApiModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}
