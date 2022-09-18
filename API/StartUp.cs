using API.DataAccess;
using API.Services.Services;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Interfaces;
using System.Text;

namespace API;
public class StartUp
{
    public StartUp(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile("config.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        this.Configuration = builder.Build();
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });
        });

        services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 10;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.SignIn.RequireConfirmedEmail = false;
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MyLFG",
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "",
                    Email = string.Empty,
                    Url = new Uri("https://www.endava.com/")
                }
            });
        });

        //services.AddDefaultIdentity<UserEntity>(options => options.SignIn.RequireConfirmedAccount = true)
        //       .AddRoles<IdentityRole>()
        //       .AddEntityFrameworkStores<DBContext>();
        
        //services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<DBContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["AppSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]))
                };
            });

        services.AddAuthentication();
        services.AddControllers();
        //services.AddDbContext<DBContext>();

        // configure DI for application services
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<IUserRepository, UserRepository>();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyLFG API V1");
            c.RoutePrefix = string.Empty;
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseCors("AllowOrigin");
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

