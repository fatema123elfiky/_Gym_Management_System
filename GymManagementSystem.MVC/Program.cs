using GymManagementSystem.DAL.Data.Contexts;

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
            builder.Services.AddDbContext<GymDbContext>();
            
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
