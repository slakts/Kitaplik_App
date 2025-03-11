using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Models
{
    public class VeriTabaniContext : IdentityDbContext
    {
        public VeriTabaniContext(DbContextOptions<VeriTabaniContext> options) : base(options) { }

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<Odunc> Oduncler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
