using BackendTaskAPI.Models;

namespace BackendTaskAPI.ApiModels
{
    public class UserApiModel : BaseApiModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Task> Tasks { get; set; }

    }
}
