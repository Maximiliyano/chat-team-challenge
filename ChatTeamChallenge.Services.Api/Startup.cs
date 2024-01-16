using ChatTeamChallenge.Application.Hubs;
using ChatTeamChallenge.Persistence;
using ChatTeamChallenge.Persistence.Infrastructure;
using ChatTeamChallenge.Services.Api.Middlewares;
using ChatTeamChallenge.Services.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ChatTeamChallenge.Services.Api;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) =>
        Configuration = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddApplication()
            .AddInfrastructure(Configuration)
            .AddPersistence(Configuration);
        
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddHealthChecks();
        
        services.AddControllers();
        
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddAutoMapper();

        services.AddCors();

        services.AddSignalR();
        
        services
            .AddMvcCore()
            .AddAuthorization()
            .AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseSwagger();
        
        app.UseSwaggerUI();
        
        AutoMigrateDatabase(app.ApplicationServices);

        app.UseCors(builder => builder
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Token-Expired")
            .WithOrigins(
                ConnectionString.SettingLocalUiKey,
                ConnectionString.SettingReleaseUiKey));

        app.UseCustomExceptionHandler();
        
        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseAuthorization();
        
        app.UseAuthentication();

        app.UseEndpoints(cfg =>
        {
            cfg.MapHealthChecks("/health");// TODO health check
            cfg.MapControllers();
            cfg.MapHub<ChatHub>("/chatHub");
        });
    }

    private static void AutoMigrateDatabase(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<ChatTeamChallengeDbContext>();

        dbContext?.Database.Migrate();
    }
}