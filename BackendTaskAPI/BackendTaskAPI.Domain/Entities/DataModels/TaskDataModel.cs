using BackendTaskAPI.Domain.DataModels;
using BackendTaskAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTaskAPI.DataModels
{
    public class TaskDataModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public NotificationDataModel Notification { get; set; }
        public string ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public ProjectDataModel Project { get; set; }
    }
}
