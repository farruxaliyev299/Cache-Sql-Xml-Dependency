using SQL_Dependency.Hubs;
using SQL_Dependency.Models;
using SQL_Dependency.SubscribeTableDependenies;

namespace SQL_Dependency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddSingleton<SubscribeProductTableDependency>();
            builder.Services.AddSingleton<SubscribeXmlFileDependency>();
            builder.Services.AddSingleton<ProductHub>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<ProductHub>("/Index");

            app.Services.GetService<SubscribeProductTableDependency>().SubscibeTableDependency();

            app.Services.GetService<SubscribeXmlFileDependency>().SubscribeXmlDependency();

            app.Run();
        }
    }
}