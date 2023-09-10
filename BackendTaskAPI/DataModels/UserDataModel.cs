using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTaskAPI.DataModels
{
    public class UserDataModel : BaseDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TasksId { get; set; }

        [ForeignKey(nameof(TasksId))]
        public ICollection<Task> Tasks { get; set; }
    }
}
