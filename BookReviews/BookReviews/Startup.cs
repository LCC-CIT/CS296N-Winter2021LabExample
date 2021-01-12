using System.Runtime.InteropServices;
using BookReviews.Models;
using BookReviews.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookReviews
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
            services.AddControllersWithViews();

            // Inject our repositories into our controllers
            services.AddTransient<IReviewRepository, ReviewRepository>(); // Generic types: Repository interface, Repository class
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Assuming that SQL Server is installed on Windows
                services.AddDbContext<BookReviewContext>(options =>
                   options.UseSqlServer(Configuration["ConnectionStrings:SQLServerConnection"]));
            }
            else
            {
                // Assuming SQLite is installed on all other operating systems
                services.AddDbContext<BookReviewContext>(options =>
                    options.UseSqlite(Configuration["ConnectionStrings:SQLiteConnection"]));
            }

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<BookReviewContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BookReviewContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.Seed(context);
        }
    }
}
