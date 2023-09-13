namespace BackendTaskAPI.EndpointRoutes
{
    public class EndpointRoute
    {
        public const string CreateTask = "api/tasks/create";

        public const string FetchTasks = "api/tasks";

        public const string FetchTask = "api/tasks/{id}";
        public const string FetchTaskByPriority = "api/tasks/priority";
        public const string FetchTaskByStatus = "api/tasks/status";
        public const string DeleteTask = "api/tasks/delete";

        public const string ModifyTask = "api/tasks/modify";

        public const string AddTaskToProject = "api/project/add-task";


        public const string CreateProject = "api/project/create";

        public const string FetchProjects = "api/projects";

        public const string FetchProject = "api/project/{id}";
        public const string DeleteProject = "api/project/delete";

        public const string ModifyProject = "api/project/modify";

        public const string RegisterUser =  "api/user/create";

        public const string FetchUser = "api/user/{id}";

        public const string ModifyUser = "api/user/modify";
        public const string DeleteUser = "api/user/delete";
    }
}
