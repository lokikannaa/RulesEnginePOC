using Microsoft.EntityFrameworkCore;
using RulesEnginePOC.MyRulesEngine;
using RulesEnginePOC.Service;
using RulesEnginePOC.Service.Interfaces;
using System.Text.Json.Serialization;

namespace RulesEnginePOC
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword;\"\r\n\"Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword;"));

            // Register services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<IRulesEngineService, RulesEngineService>();
            services.AddScoped<IEntitlementService, EntitlementService>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<UserRulesMiddleware>();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
