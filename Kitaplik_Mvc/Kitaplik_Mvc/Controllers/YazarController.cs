using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class YazarController : Controller
    {
        private readonly VeriTabaniContext _context;

        public YazarController(VeriTabaniContext context)
        {
            _context = context;
        }

        public IActionResult Listele()
        {
            var yazarList = _context.Yazarlar.Include(y => y.Kitaplar).ToList();
            return View(yazarList);
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Ekle(Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                _context.Yazarlar.Add(yazar);
                _context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(yazar);
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var yazar = _context.Yazarlar.Find(id);
            if (yazar == null)
            {
                return NotFound();
            }
            return View(yazar);
        }

        [HttpPost]
        public IActionResult Guncelle(Yazar yazar)
        {
            if (ModelState.IsValid)
            {
                var existingYazar = _context.Yazarlar.Find(yazar.Id);
                if (existingYazar == null)
                {
                    return NotFound();
                }
                existingYazar.Ad = yazar.Ad;
                existingYazar.Soyad = yazar.Soyad;
                existingYazar.DogumTarihi = yazar.DogumTarihi;
                existingYazar.Biyografi = yazar.Biyografi;
                _context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(yazar);
        }

        public IActionResult Sil(int id)
        {
            var yazar = _context.Yazarlar.Find(id);
            if (yazar == null)
            {
                return NotFound();
            }
            _context.Yazarlar.Remove(yazar);
            _context.SaveChanges();
            return RedirectToAction("Listele");
        }
    }
}
