using BackendTaskAPI.DataModels;
using BackendTaskAPI.Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BackendTaskAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<TaskDataModel> Tasks { get; set; }
        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<ProjectDataModel> Projects { get; set; }
        public DbSet<NotificationDataModel> Notifications { get; set; }




    }
}
