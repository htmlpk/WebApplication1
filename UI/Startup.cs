using AutoMapper;
using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

            var appSettingsSection = Configuration.GetSection("AppSettings");

            services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                        options.SaveToken = true;
                    });
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc()
     .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
     .SetCompatibilityVersion(CompatibilityVersion.Version_2_1); ;
            services.AddCors();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // [Entity Framework <=> Dapper] 
            BlackJack.BusinessLogicLayer.Startup.InjectServices(services, connectionString);
            BlackJack.DataAccessLayer.Startup.SetDapper(services, connectionString);
            services.Configure<IdentityOptions>(options =>
            {                
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
            });

            var mappingConvig = new MapperConfiguration(mc=>
            {
                mc.AddProfile(new MappersProfile());
            }
            );
            IMapper mapper = mappingConvig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGameService gameService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseDefaultFiles(); 
            app.UseStaticFiles(); 
            app.UseCors(builder => builder.WithOrigins(Environment.localApiUrl)); 
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

