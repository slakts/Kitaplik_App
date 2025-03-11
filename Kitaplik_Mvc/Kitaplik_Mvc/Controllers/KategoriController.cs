using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KategoriController : Controller
    {
        private readonly VeriTabaniContext _context;

        public KategoriController(VeriTabaniContext context)
        {
            _context = context;
        }

        public IActionResult Listele()
        {
            List<Kategori> kategoriList = _context.Kategoriler
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
                _context.Kategoriler.Add(kategori);
                _context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(kategori);
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var kategori = _context.Kategoriler
                .Include(k => k.Kitaplar)
                .FirstOrDefault(k => k.Id == id);
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
                var existingKategori = _context.Kategoriler.FirstOrDefault(k => k.Id == kategori.Id);
                if (existingKategori == null)
                {
                    return NotFound();
                }

                existingKategori.Isim = kategori.Isim;
                _context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(kategori);
        }

        public IActionResult Sil(int id)
        {
            var kategori = _context.Kategoriler.FirstOrDefault(k => k.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }
            _context.Kategoriler.Remove(kategori);
            _context.SaveChanges();
            return RedirectToAction("Listele");
        }
    }
}
