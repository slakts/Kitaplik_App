﻿using Kitaplik_Mvc.Models.Entities;
using Kitaplik_Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kitaplik_Mvc.Controllers
{
    public class YazarController : Controller
    {
        VeriTabaniContext context = new();

        public IActionResult Listele()
        {
            List<Yazar> yazarList = context.Yazarlar
                .Include(y => y.Kitaplar)
                .ToList();

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
                context.Yazarlar.Add(yazar);
                context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(yazar);
        }
        [HttpGet]
        public IActionResult Guncelle(int id)
        {
            var yazar = context.Yazarlar
                .Include(k => k.Kitaplar) 
                .FirstOrDefault(k => k.Id == id);
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
                var existingYazar = context.Yazarlar.FirstOrDefault(k => k.Id == yazar.Id);
                if (existingYazar == null)
                {
                    return NotFound();
                }

                existingYazar.Ad = yazar.Ad;
                existingYazar.Soyad = yazar.Soyad;
                context.SaveChanges();
                return RedirectToAction("Listele");
            }
            return View(yazar);
        }
        public IActionResult Sil(int id)
        {
            var yazar = context.Yazarlar.FirstOrDefault(k => k.Id == id);
            if (yazar == null)
            {
                return NotFound();
            }
            context.Yazarlar.Remove(yazar);
                context.SaveChanges();
            return RedirectToAction("Listele");
        }
    }
}
