using BackendTaskAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendTaskAPI.Extensions
{
    public static class ApplicationExtension
    {
        /// <summary>
        /// This will apply pending migrations to the db
        /// </summary>
        /// <param name="app"></param>
        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;

                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<ApplicationDbContext>();

                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();

                    logger.LogError("An error occurred while applying migrations. Details: {error}", ex.Message);
                }
            }
        }

        /// <summary>
        /// Configure app db context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                    migrations => migrations.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });

            // Return services for further chaining
            return services;
        }
    }
}
