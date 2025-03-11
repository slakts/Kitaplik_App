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

            // Ba�lant� dizesini g�ncelle
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Veritaban� ba�lam�n� yap�land�r
            builder.Services.AddDbContext<VeriTabaniContext>(options => options.UseSqlServer(connectionString));

            // Kimlik do�rulama yap�land�rmas�
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<VeriTabaniContext>().AddDefaultTokenProviders();


            builder.Services.AddRazorPages(); // Razor sayfalar�n� ekle
            // CORS politikas� ekle
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            // MVC servislerini ekle
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IEmailSender, EmailSender>(); // E-posta g�nderme hizmetini ekle


            var app = builder.Build();

            // HTTP iste�i i�leme yap�land�rmas�
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll"); // CORS kullan�m�

            app.UseAuthentication(); // Kimlik do�rulama
            app.UseAuthorization(); // Yetkilendirme

            app.MapRazorPages(); // Razor sayfalar�n� ekle

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
