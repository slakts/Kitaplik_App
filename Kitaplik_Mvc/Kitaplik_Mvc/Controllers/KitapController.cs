using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class KitapController : Controller
    {
        private readonly VeriTabaniContext _context;

        public KitapController(VeriTabaniContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Listele()
        {
            var kitapList = _context.Kitaplar
                                   .Include(k => k.Kategori)
                                   .Include(k => k.Yazar)
                                   .ToList();
            return View(kitapList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.KategoriValue = _context.Kategoriler
                                           .Select(k => new SelectListItem
                                           {
                                               Value = k.Id.ToString(),
                                               Text = k.Isim
                                           }).ToList();

            ViewBag.YazarValue = _context.Yazarlar
                                        .Select(y => new SelectListItem
                                        {
                                            Value = y.Id.ToString(),
                                            Text = y.AdSoyad
                                        }).ToList();

            return View();
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult Ekle(Kitap kitap, IFormFile? Image)
        {
            if (Image != null && Image.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Image.FileName)}";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                kitap.Image = $"/Images/{uniqueFileName}";
            }

            _context.Kitaplar.Add(kitap);
            _context.SaveChanges();
            return RedirectToAction("Listele");
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kitap = _context.Kitaplar.Find(id);

            if (kitap == null)
            {
                return NotFound();
            }

            ViewBag.KategoriValue = _context.Kategoriler
                                           .Select(k => new SelectListItem
                                           {
                                               Value = k.Id.ToString(),
                                               Text = k.Isim
                                           }).ToList();

            ViewBag.YazarValue = _context.Yazarlar
                                        .Select(y => new SelectListItem
                                        {
                                            Value = y.Id.ToString(),
                                            Text = y.AdSoyad
                                        }).ToList();

            return View(kitap);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap, IFormFile? Image)
        {
            var mevcutKitap = _context.Kitaplar.Find(kitap.Id);
            if (mevcutKitap == null)
            {
                return NotFound();
            }

            // Güncellenen verileri ata
            mevcutKitap.Baslik = kitap.Baslik;
            mevcutKitap.YayinTarihi = kitap.YayinTarihi;
            mevcutKitap.KategoriId = kitap.KategoriId;
            mevcutKitap.YazarId = kitap.YazarId;
            mevcutKitap.ISBN = kitap.ISBN;

            // Yeni resim yüklendi mi kontrol et
            if (Image != null && Image.Length > 0)
            {
                // Önce eski resmi sil
                if (!string.IsNullOrEmpty(mevcutKitap.Image))
                {
                    var eskiResimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mevcutKitap.Image.TrimStart('/'));
                    if (System.IO.File.Exists(eskiResimYolu))
                    {
                        System.IO.File.Delete(eskiResimYolu);
                    }
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(Image.FileName)}";
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }

                mevcutKitap.Image = $"/Images/{uniqueFileName}";
            }

            _context.SaveChanges();
            return RedirectToAction("Listele");
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int id)
        {
            var kitap = _context.Kitaplar.Find(id);
            if (kitap != null)
            {
                // Eski resmi sil
                if (!string.IsNullOrEmpty(kitap.Image))
                {
                    var eskiResimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", kitap.Image.TrimStart('/'));
                    if (System.IO.File.Exists(eskiResimYolu))
                    {
                        System.IO.File.Delete(eskiResimYolu);
                    }
                }

                _context.Kitaplar.Remove(kitap);
                _context.SaveChanges();
            }

            return RedirectToAction("Listele");
        }
    }
}
