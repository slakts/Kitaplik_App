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
            List<Kitap> kitapList = context.Kitaplar
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
                                           Text = y.Ad
                                       }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Kitap kitap)
        {

            if (ModelState.IsValid)
            {
                context.Kitaplar.Add(kitap);
                context.SaveChanges();
                return RedirectToAction("Listele");
            }

            // Hata durumunda ViewBag'i tekrar doldur
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
                                           Text = y.Ad
                                       }).ToList();

            return View(kitap);
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
                                           Text = y.Ad
                                       }).ToList();

            return View(kitap);
        }

        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                context.Kitaplar.Update(kitap);
                context.SaveChanges();
                return RedirectToAction("Listele");
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
                                           Text = y.Ad
                                       }).ToList();

            return View(kitap);
        }

        public IActionResult Sil(int id)
        {
            var kitap = context.Kitaplar.Find(id);
            if (kitap != null)
            {
                context.Kitaplar.Remove(kitap);
                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}
