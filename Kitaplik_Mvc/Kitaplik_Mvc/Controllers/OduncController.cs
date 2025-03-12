using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class OduncController : Controller
    {
        private readonly VeriTabaniContext _context;

        public OduncController(VeriTabaniContext context)
        {
            _context = context;
        }

        public IActionResult Listele()
        {
            var oduncList = _context.Oduncler.Include(o => o.Kitap).ToList();
            return View(oduncList);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            ViewBag.KitapList = _context.Kitaplar
                .Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Baslik
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Odunc odunc)
        {
            
                odunc.OduncAlimTarihi = DateTime.Now;
                _context.Oduncler.Add(odunc);
                _context.SaveChanges();
                TempData["basarili"] = "Yeni ödünç kaydı başarıyla oluşturuldu!";
                return RedirectToAction("Listele");
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var odunc = _context.Oduncler.FirstOrDefault(o => o.Id == id);
            if (odunc == null)
            {
                return NotFound();
            }

            ViewBag.KitapList = _context.Kitaplar
                .Select(k => new SelectListItem
                {
                    Value = k.Id.ToString(),
                    Text = k.Baslik
                }).ToList();

            return View(odunc);
        }

        [HttpPost]
        public IActionResult Guncelle(Odunc odunc)
        {
                _context.Oduncler.Update(odunc);
                _context.SaveChanges();
                TempData["basarili"] = "Ödünç kaydı başarıyla güncellendi!";
                return RedirectToAction("Listele");
        }

        public IActionResult Sil(int id)
        {
            var odunc = _context.Oduncler.FirstOrDefault(o => o.Id == id);
            if (odunc == null)
            {
                return NotFound();
            }

            _context.Oduncler.Remove(odunc);
            _context.SaveChanges();
            TempData["basarili"] = "Ödünç kaydı başarıyla silindi!";
            return RedirectToAction("Listele");
        }
    }
}
