using BackendTaskAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTaskAPI.DataModels
{
    public class ProjectDataModel : BaseModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        public string TaskId { get; set; }

        public List<TaskDataModel> Tasks { get; set; }
    }
}
