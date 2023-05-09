using Ampulheta.Domain.Intefaces.Repositories;
using Ampulheta.Domain.Intefaces.Services;
using Ampulheta.Domain.Notification;
using Ampulheta.Domain.Services;
using Ampulheta.Repository.Repositories;
using MediatR;

namespace Ampulheta.WebApi.Config
{
    public static class IocConfiguration
    {
        public static IServiceCollection ConfigureIoc(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ConfigureServices(services);
            ConfigureRepositories(services);
            ConfigureNotify(services); 
            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimeRepository, TimeRepository>();
            return services;
        }

        private static IServiceCollection ConfigureNotify(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
            return services;
        }
    }
}
