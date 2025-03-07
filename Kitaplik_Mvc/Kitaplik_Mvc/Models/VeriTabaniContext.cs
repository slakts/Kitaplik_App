using Kitaplik_Mvc.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Models
{
    public class VeriTabaniContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Kütüphane_MVC;Trusted_Connection=True;TrustServerCertificate=true");
        }

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Odunc> Oduncler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public string Ad { get; internal set; }
        public string Soyad { get; internal set; }
    }
}
