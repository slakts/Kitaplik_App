using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Kitaplik_Mvc.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Kitaplik_Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Baðlantý dizesini güncelle
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Veritabaný baðlamýný yapýlandýr
            builder.Services.AddDbContext<VeriTabaniContext>(options => options.UseSqlServer(connectionString));

            // Kimlik doðrulama yapýlandýrmasý
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<VeriTabaniContext>().AddDefaultTokenProviders();


            builder.Services.AddRazorPages(); // Razor sayfalarýný ekle
            // CORS politikasý ekle
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // MVC servislerini ekle
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IEmailSender, EmailSender>(); // E-posta gönderme hizmetini ekle


            var app = builder.Build();

            // HTTP isteði iþleme yapýlandýrmasý
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll"); // CORS kullanýmý

            app.UseAuthentication(); // Kimlik doðrulama
            app.UseAuthorization(); // Yetkilendirme

            app.MapRazorPages(); // Razor sayfalarýný ekle

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
