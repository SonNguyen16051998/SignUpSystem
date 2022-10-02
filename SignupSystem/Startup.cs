using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using SignupSystem.Models;
using SignupSystem.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem
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
            services.AddControllersWithViews()
                 .AddNewtonsoftJson(options =>
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

            services.AddDbContext<DataContext>(option =>
               option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IStudent,StudentSvc>();
            services.AddTransient<ICourse,CourseSvc>();
            services.AddTransient<ITeacher,TeacherSvc>();
            services.AddTransient<ISubjectDepartment, SubjectDepartmentSvc>();
            services.AddTransient<ISubject, SubjectSvc>();
            services.AddTransient<IClass, ClassSvc>();
            services.AddTransient<IStudentClass, Student_ClassSvc>();
            services.AddTransient<ISubjectPointType,Subject_PointTypeSvc>();
            services.AddTransient<IPointType,PointTypeSvc>();
            services.AddTransient<IHoliday, HolidaySvc>();
            services.AddTransient<IThongKe,ThongKeSvc>();
            services.AddTransient<IKhoa,KhoaSvc>();
            services.AddTransient<IRole, RoleSvc>();
            services.AddTransient<IQuyen,QuyenSvc>();
            services.AddTransient<IRole_Quyen,Role_QuyenSvc>();
            services.AddTransient<IUser_Role,User_RoleSvc>();
            services.AddTransient<IUser,UserSvc>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidAudience = Configuration["Jwt:Audience"],
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });

            services.AddControllersWithViews();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SignupSystem", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            /*services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));*/
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Student", policy =>
                {
                    policy.RequireClaim("Role", "Student");
                });
                options.AddPolicy("Teacher", policy =>
                {
                    policy.RequireClaim("Role", "Teacher");
                });
                options.AddPolicy("User", policy =>
                {
                    policy.RequireClaim("Role", "User");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SignupSystem v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            /*app.UseCors("MyPolicy");*/

            app.UseAuthentication();    

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
