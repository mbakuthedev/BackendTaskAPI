using BackendTaskAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTaskAPI.Domain.DataModels
{
    public class UserDataModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string TasksId { get; set; }

        [ForeignKey(nameof(TasksId))]
        public ICollection<Task> Tasks { get; set; }
    }
}
