using BackendTaskAPI.Models;

namespace BackendTaskAPI.ApiModels
{
    public class NotificationsApiModel : BaseApiModel
    {
        public DateTime DueDate { get; set; }
        public string NotificationMessage { get; set; }
        public string StatusUpdate { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
