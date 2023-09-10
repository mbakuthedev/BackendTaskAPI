namespace BackendTaskAPI.DataModels
{
    public class ProjectDataModel
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        public TaskDataModel Tasks { get; set; }
    }
}
