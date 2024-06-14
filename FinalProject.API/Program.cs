namespace FinalProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // aq wtapperis pontshi qna
            builder.AddDatabaseContext();
            builder.ConfigureJwtOptions();
            builder.AddIdentity();
            builder.AddAuthentication();
            builder.AddScopedServices();
            builder.AddControllers();
            builder.AddEndpointsApiExplorer();
            builder.AddSwagger();
            builder.Services.AddAuthorization();
            builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
            builder.Services.AddHostedService<PostCheckerBackgroundService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // authentications vaketeb
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


//// This method gets called by the runtime. Use this method to add services to the container.
//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddControllers();


//    services.AddAuthentication(options =>
//    {
//        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false; // Set this to true for production
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Security:SecretKey").Value)),
//            ValidateIssuer = false,
//            ClockSkew = TimeSpan.FromMinutes(0.1),
//            ValidateAudience = false
//        };
//    });

//    services.AddAuthorization();

//    services.AddCors(options =>
//    {
//        options.AddDefaultPolicy(builder =>
//        {
//            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
//        });
//    });

//}