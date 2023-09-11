using BackendTaskAPI.ApiModels;
using BackendTaskAPI.DataModels;

namespace BackendTaskAPI.Models
{
    public class TaskApiModel : BaseApiModel
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public NotificationsApiModel Notification { get; set; }
    }
}
