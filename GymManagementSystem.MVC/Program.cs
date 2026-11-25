using GymManagementSystem.BLL.Profiles;
using GymManagementSystem.BLL.Services.Members.Classes;
using GymManagementSystem.BLL.Services.Members.Interfaces;
using GymManagementSystem.BLL.Services.Sessions.Classes;
using GymManagementSystem.BLL.Services.Sessions.Interfaces;
using GymManagementSystem.DAL.Data.Contexts;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.Classes;
using GymManagementSystem.DAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /*builder.Services is the list of registrations. AddDbContext<GymDbContext>() adds this 
             * entry to it: “GymDbContext: create one per request (Scoped lifetime), 
             * and dispose of it automatically when the request ends.”
             */

            // ── PART 1: registration — tell the container what exists ──
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // add to clr the gymdbcontext to the dependency injection container
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // before
            //builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            //after
            //builder.Services.AddScoped<IRepository<Plan>, Repository<Plan>>();
            //builder.Services.AddScoped<IRepository<Member>, Repository<Member>>();
            //builder.Services.AddScoped<IRepository<Session>, Repository<Session>>();
            // instead of many lines , we could maeke that line
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // whatever any entity passed to it
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddAutoMapper(x=>x.AddProfile(new MemberProfiles()));
            builder.Services.AddAutoMapper(x=>x.AddProfile(new SessionProfile()));

            var app = builder.Build();

            // ── PART 2: the pipeline — the middleware each request passes through ──
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
