﻿namespace BackendTaskAPI.Models
{
    public class TaskApiModel : BaseApiModel
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
