namespace BackendTaskAPI.Models
{
    public abstract class BaseApiModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } 
    }
}
