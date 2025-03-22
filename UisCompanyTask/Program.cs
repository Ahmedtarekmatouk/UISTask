using Microsoft.EntityFrameworkCore;
using UisCompanyTask.IService;
using UisCompanyTask.Models;
using UisCompanyTask.Repository;
using UisCompanyTask.Services;
using UisCompanyTask.UnitOfWork;

namespace UisCompanyTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<UisProductContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            builder.Services.AddScoped<IProductservice, ProductService>();
            builder.Services.AddScoped<IProductTransactionService, ProductTransactionService>();
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
                pattern: "{controller=ProductTransaction}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
