using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTaskAPI.DataModels
{
    public class ProjectDataModel : BaseDataModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        public string TaskId { get; set; }

        [ForeignKey(nameof(TaskId))]
        public TaskDataModel Tasks { get; set; }
    }
}
