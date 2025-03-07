using Kitaplik_Mvc.Models;
using Kitaplik_Mvc.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kitaplik_Mvc.Controllers
{
    public class UyeController : Controller
    {
        VeriTabaniContext context = new();

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(Uye uye)
        {
            var data = context.Uyeler.FirstOrDefault(x => x.Email == uye.Email && x.Sifre == uye.Sifre);
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
                context.Uyeler.Add(uye);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(uye);
        }

        public IActionResult Listele()
        {
            List<Uye> uyeList = context.Uyeler.ToList();
            return View(uyeList);
        }

        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var uye = context.Uyeler.Find(id);
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
                context.Uyeler.Update(uye);
                context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(uye);
        }

        public IActionResult Sil(int id)
        {
            var uye = context.Uyeler.Find(id);
            if (uye != null)
            {
                context.Uyeler.Remove(uye);
                context.SaveChanges();
            }
            return RedirectToAction("Listele");
        }
    }
}
