using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using si2.bll.Helpers;
using si2.bll.Services;
using si2.dal.Context;
using si2.dal.Entities;
using si2.dal.Repositories;
using si2.dal.UnitOfWork;
using System;
using System.Linq;
using System.Text;

namespace si2.api
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
            services.AddDbContextPool<Si2DbContext>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Si2ConnectionString"));
                options.UseInternalServiceProvider(provider);
            });
            
            services.AddEntityFrameworkSqlServer();
           
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<Si2DbContext>()
            .AddDefaultTokenProviders(); 
            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
    
            services.AddTransient<IDataflowRepository, DataflowRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IDataflowVehicleRepository, DataflowVehicleRepository>();

            services.AddTransient<IProgramLevelRepository, ProgramLevelRepository>();

            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IContactInfoRepository, ContactInfoRepository>();
            services.AddTransient<IProgramRepository, ProgramRepository>();

            services.AddTransient<IDocumentRepository, DocumentRepository>();

            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<ICohortRepository, CohortRepository>();
            services.AddTransient<IUserCohortRepository, UserCohortRepository>();

            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IUserCourseRepository, UserCourseRepository>();
            services.AddTransient<ICourseCohortRepository, CourseCohortRepository>();

            services.AddTransient<IServiceBase, ServiceBase>();
            services.AddTransient<IDataflowService, DataflowService>();

            services.AddTransient<IProgramLevelService, ProgramLevelService>();

            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IContactInfoService, ContactInfoService>();
            services.AddTransient<IInstitutionService, InstitutionService>();
            services.AddTransient<IProgramService, ProgramService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<ICohortService, CohortService>();
            services.AddTransient<IUserCohortService, UserCohortService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IUserCourseService, UserCourseService>();
            services.AddTransient<ICourseCohortService, CourseCohortService>();

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IDataSeeder, DataSeeder>();

            services.AddAuthentication()
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration.GetValue<string>("Si2JwtBearerConstants:Issuer"),
                        ValidAudience = Configuration.GetValue<string>("Si2JwtBearerConstants:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Si2JwtBearerConstants:Key")))
                    };
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers()
                .AddNewtonsoftJson(setupAction => setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Register the Swagger generator, defining 1 or more Swagger documents
            //var versionInfo = FileVersionInfo.GetVersionInfo(Directory.GetCurrentDirectory() + "\\" + "si2.api.dll");
            //var versionInfo = FileVersionInfo.GetVersionInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + "si2.api.dll");
            //var version = "VN : " + versionInfo.FileVersion + " - " + DateTime.UtcNow.ToString("M/d/yyyy HH:mm");
                          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = DateTime.UtcNow.ToString("MM/dd/yyyy h:mm tt"),
                    Title = "Université Saint-Joseph de Beyrouth - SI2 Server"
                    //Description = "The university Web API for handling students registrations",
                    //TermsOfService = new Uri("https://www.facebook.com/usj.edu.lb/videos/890474227787534/"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "John Smith",
                    //    Email = "John.Smit@email.com",
                    //    Url = new Uri("https://twitter.com/usjliban?lang=en"),
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Use under USJ-LICX",
                    //    Url = new Uri("https://example.com/license"),
                    //}
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //added for the api versioning
            });

            //add api versioning
            services.AddControllers();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDataSeeder dataSeeder, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //dataSeeder.SeedRoles().SeedUsers();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json" changed for the api versioning
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //loggerFactory.AddFile("Logs/myapp-{Date}.log");
        }
    }
}
