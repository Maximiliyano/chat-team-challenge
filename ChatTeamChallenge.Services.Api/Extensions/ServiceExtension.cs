using System.Reflection;
using System.Text;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Application.Core.Behaviours;
using ChatTeamChallenge.Application.Disputes;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Reviews;
using ChatTeamChallenge.Infrastructure.JWT;
using ChatTeamChallenge.Infrastructure.MappingProfiles;
using ChatTeamChallenge.Persistence;
using ChatTeamChallenge.Persistence.Infrastructure;
using ChatTeamChallenge.Persistence.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ChatTeamChallenge.Services.Api.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            cfg.RegisterServicesFromAssemblies(typeof(ICommandHandler<,>).Assembly);
        });
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
        
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        
        services.ConfigureJwt(configuration);
        
        services.AddScoped<JwtIssuerOptions>();
        
        services.AddScoped<IJwtFactory, JwtFactory>();

        services.AddTransient<IMessageService, MessageService>();
        
        services.AddTransient<IUserService, UserService>();
        
        services.AddTransient<IAuthService, AuthService>();
        
        services.AddTransient<IChatService, ChatService>();
        
        services.AddTransient<IChatMemberService, ChatMemberService>();
        
        return services;
    }
    
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionString.SettingKey);
        
        services.AddDbContext<ChatTeamChallengeDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<ChatTeamChallengeDbContext>());

        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ChatTeamChallengeDbContext>());

        services.AddScoped<IChatRepository, ChatRepository>();

        services.AddScoped<IChatMemberRepository, ChatMemberRepository>();
        
        services.AddScoped<IMessageRepository, MessageRepository>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<MessageProfile>();
            cfg.AddProfile<RefreshTokenProfile>();
            cfg.AddProfile<ChatProfile>();
            cfg.AddProfile<ChatMemberProfile>();
        },
        Assembly.GetExecutingAssembly());
    }
    
    private static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration["JwtIssuerOptions:SecretKey"];
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

        services.Configure<JwtIssuerOptions>(options =>
        {
            options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)]!;
            options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)]!;
            options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        });

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

            ValidateAudience = true,
            ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });
    }
}