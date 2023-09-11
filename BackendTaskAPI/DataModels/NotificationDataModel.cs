namespace BackendTaskAPI.DataModels
{
    public class NotificationDataModel : BaseDataModel
    {
        public DateTime DueDate { get; set; }
        public string NotificationMessage { get; set; }
        public string StatusUpdate { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Read { get; set; }
        public bool Unread { get; set; }
    }
}
