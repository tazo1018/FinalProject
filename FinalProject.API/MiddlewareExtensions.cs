using FinalProject.Contracts;
using FinalProject.Contracts.Comments;
using FinalProject.Contracts.Posts;
using FinalProject.Contracts.Users;
using FinalProject.Data;
using FinalProject.Models.Identity;
using FinalProject.Repositories;
using FinalProject.Service.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace FinalProject.API
{
    public static class MiddlewareExtensions
    {
        public static void AddDatabaseContext(this WebApplicationBuilder builder) => builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerLocalConnection")));

        public static void ConfigureJwtOptions(this WebApplicationBuilder builder) => builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));


        public static void AddIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 3;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireDigit = false;

                Options.User.RequireUniqueEmail = true; 
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddAuthentication(this WebApplicationBuilder builder)
        {

            var secret = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret); 

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, 

                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }



        public static void AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddScoped<IPostServices, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtGenerator, JwtTokenGenerator>();
        }
        public static void AddControllers(this WebApplicationBuilder builder) => builder.Services.AddControllers();
        public static void AddEndpointsApiExplorer(this WebApplicationBuilder builder) => builder.Services.AddEndpointsApiExplorer();
        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FinalProject", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
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
                 new List<string>()
            }
        });
            });
        }
    }
}
