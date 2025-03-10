using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class OduncController : Controller
    {
        private readonly VeriTabaniContext context = new();

        public IActionResult Listele()
        {
            var oduncList = context.Oduncler
                                    .Include(o => o.Kitap)
                                    .Include(o => o.Uye)
                                    .ToList();
            return View(oduncList);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.KitapValue = context.Kitaplar
                                         .Select(k => new SelectListItem
                                         {
                                             Value = k.Id.ToString(),
                                             Text = k.Baslik
                                         }).ToList();

            ViewBag.UyeValue = context.Uyeler
                                       .Select(u => new SelectListItem
                                       {
                                           Value = u.Id.ToString(),
                                           Text = u.Ad + " " + u.Soyad
                                       }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Odunc odunc)
        {
            odunc.OduncAlimTarihi = DateTime.Now;
            context.Oduncler.Add(odunc);
            context.SaveChanges();
            return RedirectToAction("Listele");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var odunc = context.Oduncler
                               .Include(o => o.Kitap)
                               .Include(o => o.Uye)
                               .FirstOrDefault(o => o.Id == id);

            if (odunc == null)
            {
                return NotFound();
            }

            ViewBag.KitapValue = context.Kitaplar
                                         .Select(k => new SelectListItem
                                         {
                                             Value = k.Id.ToString(),
                                             Text = k.Baslik
                                         }).ToList();

            ViewBag.UyeValue = context.Uyeler
                                       .Select(u => new SelectListItem
                                       {
                                           Value = u.Id.ToString(),
                                           Text = u.Ad + " " + u.Soyad
                                       }).ToList();

            return View(odunc);
        }

        [HttpPost]
        public IActionResult Guncelle(Odunc odunc)
        {
            var mevcutOdunc = context.Oduncler.Find(odunc.Id);
            if (mevcutOdunc == null)
            {
                return NotFound();
            }

            mevcutOdunc.KitapID = odunc.KitapID;
            mevcutOdunc.UyeID = odunc.UyeID;
            mevcutOdunc.OduncAlimTarihi = odunc.OduncAlimTarihi;
            mevcutOdunc.IadeTarihi = odunc.IadeTarihi;

            context.SaveChanges();
            return RedirectToAction("Listele");
        }

        public IActionResult Sil(int id)
        {
            var odunc = context.Oduncler
                               .Include(o => o.Kitap)
                               .FirstOrDefault(o => o.Id == id);

            if (odunc != null)
            {
                context.Oduncler.Remove(odunc);
                context.SaveChanges();
            }

            return RedirectToAction("Listele");
        }
    }
}
