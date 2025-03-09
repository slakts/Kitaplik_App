using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class KitapController : Controller
    {
        VeriTabaniContext context = new();

        public IActionResult Listele()
        {
            var kitapList = context.Kitaplar
                                   .Include(k => k.Kategori)
                                   .Include(k => k.Yazar)
                                   .ToList();
            return View(kitapList);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.KategoriValue = context.Kategoriler
                                           .Select(k => new SelectListItem
                                           {
                                               Value = k.Id.ToString(),
                                               Text = k.Isim
                                           }).ToList();

            ViewBag.YazarValue = context.Yazarlar
                                        .Select(y => new SelectListItem
                                        {
                                            Value = y.Id.ToString(),
                                            Text = y.AdSoyad
                                        }).ToList();

            return View();
        }

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

            context.Kitaplar.Add(kitap);
            context.SaveChanges();
            return RedirectToAction("Listele");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kitap = context.Kitaplar.Find(id);

            if (kitap == null)
            {
                return NotFound();
            }

            ViewBag.KategoriValue = context.Kategoriler
                                           .Select(k => new SelectListItem
                                           {
                                               Value = k.Id.ToString(),
                                               Text = k.Isim
                                           }).ToList();

            ViewBag.YazarValue = context.Yazarlar
                                        .Select(y => new SelectListItem
                                        {
                                            Value = y.Id.ToString(),
                                            Text = y.AdSoyad
                                        }).ToList();

            return View(kitap);
        }

        [HttpPost]
        public IActionResult Guncelle(Kitap kitap, IFormFile? Image)
        {
            var mevcutKitap = context.Kitaplar.Find(kitap.Id);
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

            context.SaveChanges(); // Update işlemi gereksiz
            return RedirectToAction("Listele");
        }

        public IActionResult Sil(int id)
        {
            var kitap = context.Kitaplar.Find(id);
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

                context.Kitaplar.Remove(kitap);
                context.SaveChanges();
            }

            return RedirectToAction("Listele");
        }
    }
}
