using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Realtor.Core;
using Realtor.Data;
using Realtor.Core.Entities;
using Realtor.Extensions.DependencyInjection;
using Realtor.Api.Infrastructure.Extensions.DependencyInjection;
using Realtor.Application.Extensions.DependencyInjection;

namespace Realtor.Api.Infrastructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddDatabase(Configuration.GetConnectionString("DefaultConnection"));
            services.AddMapper();
            services.AddMediation();
            services.AddCors(options =>
            {
                List<string> origins = Configuration.GetSection("CorsOrigins").Get<List<string>>();
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(origins.ToArray()).AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddControllers().AddNewtonsoftJson()/*.AddValidation();*/;

            byte[] key = Encoding.ASCII.GetBytes(Constants.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            IServiceScopeFactory scopeFactory =
                                context.HttpContext.RequestServices.GetService<IServiceScopeFactory>();
                            Users user;
                            Guid userId = Guid.Parse(context.Principal.Identity.Name);

                            using (IServiceScope scope = scopeFactory.CreateScope())
                            {
                                ApplicationDbContext db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                                user = db.Users.Find(userId);
                            }

                            if (user == null)
                            {
                                // Return unauthorized if user no longer exists
                                context.Fail("Unauthorized");
                            }

                            return Task.CompletedTask;
                        }
                    };
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddUser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();
            app.UseExceptionHandler("/error");
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseStaticFiles();
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "images")),
                RequestPath = new PathString("/images")
            });
        }
    }
}

