﻿using BackendTaskAPI.Models;

namespace BackendTaskAPI.DataModels
{
    public class TaskDataModel : BaseDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public NotificationDataModel Notification { get; set; }
        public ProjectDataModel Project { get; set; }
    }
}
