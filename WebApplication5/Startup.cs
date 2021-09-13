using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using WebApplication5.Security;

namespace WebApplication5
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       // public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<AppDbContext>();
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            services.AddDbContextPool<AppDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            services.AddControllersWithViews();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();  // MockEmployeeRepository          
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolesPolicy",
                    policy => policy.RequireClaim("Delete Roles", "sim")
                                    .RequireRole("Super Admin"));
                options.AddPolicy("CreateRolesPolicy",
                    policy => policy.RequireClaim("Create Roles", "sim"));
            
                    //policy => policy.RequireAssertion(context =>
                        //context.User.IsInRole("Admin") && 
                        //context.User.HasClaim(claim => claim.Type == "Create Roles" && claim.Value == "sim") ||
                        //context.User.IsInRole("SuperAdmin")
                  
                options.AddPolicy("EditRolesPolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));


                //options.AddPolicy("AdminRolesPolicy",
                //policy => policy.RequireClaim("Admin", "Test") 
                //);
            });
            services.AddSingleton<IAuthorizationHandler, EditClaimsHandler>();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Admin/AccessDenied");
            });

            services.AddAuthentication()
                .AddGoogle(options => 
                {
                    options.ClientId = "586234923771-mqbcib3rtod1sseif8lh131qmvgd8jip.apps.googleusercontent.com";
                    options.ClientSecret = "CRJe4n-IN_V-ToAEZC71FWNg";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
                name: "employeeScreen",
                pattern: "{controller=Management}/{action=List}/{id?}");
            }); 
        }
    }
}
