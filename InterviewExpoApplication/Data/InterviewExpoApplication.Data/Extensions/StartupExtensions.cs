using InterviewExpoApplication.Data.Contexts;
using InterviewExpoApplication.Data.Repositories;
using InterviewExpoApplication.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InterviewExpoApplication.Data.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterDatabaseProvider(this IServiceCollection services, DatabaseConfiguration databaseConfiguration)
        {
            switch (databaseConfiguration.Provider)
            {
                case DatabaseProvider.Sqlite:
                    {
                        services.AddDbContext<MainContext>(options => options.UseSqlite(databaseConfiguration.ConnectionString, b => b.MigrationsAssembly("InterviewExpoApplication.Data.Sqlite")));
                    }
                    break;
                case DatabaseProvider.SqlServer:
                    {
                        services.AddDbContext<MainContext>(options => options.UseSqlServer(databaseConfiguration.ConnectionString, b => b.MigrationsAssembly("InterviewExpoApplication.Data.SqlServer")));
                    }
                    break;
                default: throw new ArgumentException("Invalid database provider set");
            }

            services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            return services;
        }
    }
}
