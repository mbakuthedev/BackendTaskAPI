using BackendTaskAPI.Models;

namespace BackendTaskAPI.ApiModels
{
    public class ProjectApiModel : BaseApiModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public IList<Task> Tasks { get; set; }
    }
}
