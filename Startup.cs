using CMPApiMicroservice.DBContext;
using CMPApiMicroservice.Extensions;
using CMPApiMicroservice.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMPApiMicroservice.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CMPApiMicroservice.Service;

namespace CMPApiMicroservice
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

            services.AddControllers();

            #region Sql Configuration

            services.AddDbContext<DBEntities>(o => o.UseSqlServer(Configuration.GetConnectionString("CampaignDB")));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddHealthChecks().AddSqlServer(Configuration["ConnectionStrings:CampaignDB"]);

            #endregion

            #region JWT Authentication

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSetting = appSettingSection.Get<AppSettings>();
            var secretKey = Encoding.ASCII.GetBytes(appSetting.SecretKey);
            services.AddAuthentication(au => {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            #endregion

            services.AddSingleton<IAuthenticateService, AuthenticateService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CMPApiMicroservice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            app.ConfigurExceptionHandler(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHealthChecks();
            });
        }
    }
}
