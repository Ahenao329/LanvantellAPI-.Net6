using LavantellAPIS.Context;
using LavantellAPIS.Models;
using LavantellAPIS.Repositories;
using LavantellAPIS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
//using LavantellAPIS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavantellAPIS
{
    public class Startup
    {
        readonly string MiCors = "MiCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy(name: MiCors,
                                  builder =>
                                  {
                                      builder.WithHeaders("*");
                                      builder.WithOrigins("*");
                                      builder.WithMethods("*");
                                  });
            });
            services.AddControllers();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);


            //jwt
            var appSettings = appSettingsSection.Get<AppSettings>();
            var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);
            services.AddAuthentication(d =>
            {
                d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(d =>
             {
                 d.RequireHttpsMetadata = false;
                 d.SaveToken = true;
                 d.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(llave),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };

             });
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<InosotrosRepository, LavantellRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LavantellAPIS", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddCors(options => options.AddPolicy("AllowWebApp",
                                            builder => builder.AllowAnyOrigin()
                                                               .AllowAnyHeader()
                                                               .AllowAnyMethod()));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LavantellAPIS v1"));
            }
            //app.UseCors("AllowWebApp");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MiCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
