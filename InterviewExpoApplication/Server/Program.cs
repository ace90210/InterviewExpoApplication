using InterviewExpoApplication.Data.Contexts;
using InterviewExpoApplication.Data.Repositories;
using InterviewExpoApplication.Shared.Configuration;
using InterviewExpoApplication.Shared.JsonConverters;
using Microsoft.EntityFrameworkCore;
using InterviewExpoApplication.Data.Extensions;
using InterviewExpoApplication.Server.Services;

namespace InterviewExpoApplication.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });


            builder.Services.AddRazorPages();

            builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection("DatabaseConfiguration"));
            builder.Services.Configure<ApplicationConfiguration>(builder.Configuration.GetSection("ApplicationConfiguration"));
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddHttpClient();

            var databaseConfiguration =
                builder.Configuration.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();

            builder.Services.RegisterDatabaseProvider(databaseConfiguration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}