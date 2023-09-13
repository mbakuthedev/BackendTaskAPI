using BackendTaskAPI.Models;

namespace BackendTaskAPI.ApiModels
{
    public class ProjectApiModel : BaseModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string TaskId { get; set; }
    }
}
