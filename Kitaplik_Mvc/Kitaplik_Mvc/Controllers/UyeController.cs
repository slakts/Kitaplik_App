using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kitaplik_Mvc.Controllers
{
    public class UyeController : Controller
    {
        private readonly VeriTabaniContext _context;

        public UyeController(VeriTabaniContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Uye uye)
        {
            var data = _context.Uyeler.FirstOrDefault(x => x.Email == uye.Email && x.Sifre == uye.Sifre);
            if (data == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult UyeOl()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UyeOl(Uye uye)
        {
            if (ModelState.IsValid)
            {
                _context.Uyeler.Add(uye);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(uye);
        }

        public IActionResult Listele()
        {
            List<Uye> uyeList = _context.Uyeler.ToList();
            return View(uyeList);
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var uye = _context.Uyeler.Find(id);
            if (uye == null)
            {
                return NotFound();
            }
            return View(uye);
        }

        [HttpPost]
        public IActionResult Guncelle(Uye uye)
        {
            if (ModelState.IsValid)
            {
                _context.Uyeler.Update(uye);
                _context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(uye);
        }

        public IActionResult Sil(int id)
        {
            var uye = _context.Uyeler.Find(id);
            if (uye != null)
            {
                _context.Uyeler.Remove(uye);
                _context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}
