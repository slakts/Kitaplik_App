using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class KategoriController : Controller
    {
        VeriTabaniContext context = new();
        public IActionResult Listele()
        {
            List<Kategori> kategoriList = context.Kategoriler
                .Include(k => k.Kitaplar)
                .ToList();
            return View(kategoriList);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                context.Kategoriler.Add(kategori);
                context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(kategori);
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kategori = context.Kategoriler
                .Include(k => k.Kitaplar)
                .FirstOrDefault(k => k.Id == id); ;
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        public IActionResult Guncelle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                var existingKategori = context.Kategoriler.FirstOrDefault(k => k.Id == kategori.Id);
                if (existingKategori == null)
                {
                    return NotFound();
                }

                existingKategori.Isim = kategori.Isim;
                context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(kategori);
        }

        public IActionResult Sil(int id)
        {
            var kategori = context.Kategoriler.FirstOrDefault(k => k.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }
            context.Kategoriler.Remove(kategori);
            context.SaveChanges();
            return RedirectToAction("Listele");
        }
    }
}
