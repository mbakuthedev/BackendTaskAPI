using BackendTaskAPI.Models;

namespace BackendTaskAPI.Domain.DataModels
{
    public class NotificationDataModel : BaseModel
    {
        public DateTime DueDate { get; set; }
        public string NotificationMessage { get; set; }
        public string StatusUpdate { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Read { get; set; }
        public bool Unread { get; set; }
    }
}
