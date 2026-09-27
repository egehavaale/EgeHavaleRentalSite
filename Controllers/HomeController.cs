using EgeHavaleProjeOdevi.Data;
using EgeHavaleProjeOdevi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EgeHavaleProjeOdevi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Evler = await _context.Evler.Where(x => x.Aktif).Take(4).ToListAsync(),
                Vasitalar = await _context.Vasitalar.Where(x => x.Aktif).Take(4).ToListAsync(),
                Esyalar = await _context.Esyalar.Where(x => x.Aktif).Take(4).ToListAsync(),
                IsIlanlari = await _context.IsIlanlari.Where(x => x.Aktif).Take(4).ToListAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> Evler() => View(await _context.Evler.Where(e => e.Aktif).ToListAsync());
        public async Task<IActionResult> Vasita() => View(await _context.Vasitalar.Where(v => v.Aktif).ToListAsync());
        public async Task<IActionResult> IkinciEl() => View(await _context.Esyalar.Where(e => e.Aktif).ToListAsync());
        public async Task<IActionResult> IsIlanlari() => View(await _context.IsIlanlari.Where(i => i.Aktif).ToListAsync());

      

        public async Task<IActionResult> Details(int id) 
        {
            var item = await _context.Evler.FindAsync(id);
            if (item == null) return NotFound();

            PrepareDetailsViewBag(item, "Emlak");
            return View("Details");
        }

        public async Task<IActionResult> VasitaDetails(int id) 
        {
            var item = await _context.Vasitalar.FindAsync(id);
            if (item == null) return NotFound();

            PrepareDetailsViewBag(item, "Vasita");
            return View("Details");
        }

        public async Task<IActionResult> EsyaDetails(int id)
        {
            var item = await _context.Esyalar.FindAsync(id);
            if (item == null) return NotFound();

            PrepareDetailsViewBag(item, "Esya");
            return View("Details");
        }

        private void PrepareDetailsViewBag(dynamic item, string tip)
        {
            ViewBag.Id = item.Id;
            ViewBag.Ad = item.Ad;
            ViewBag.FiyatDecimal = item.Fiyat;
            ViewBag.Fiyat = item.Fiyat.ToString("C");
            ViewBag.Adres = item.Adres;
            ViewBag.Detaylar = item.Detaylar;
            ViewBag.GorselUrl = item.GorselUrl;
            ViewBag.Kimden = item.Kimden;
            ViewBag.IlanTipi = tip;

            if (tip == "Emlak")
            {
                ViewBag.OdaSayisi = item.OdaSayisi;
                ViewBag.Metrekare = item.Metrekare;
                ViewBag.Isitma = item.Isitma;
                ViewBag.Esyali = item.Esyali ? "Evet" : "Hayır";
            }
            else if (tip == "Vasita")
            {
                ViewBag.ModelYili = item.ModelYili;
                ViewBag.Kilometre = item.Kilometre;
                ViewBag.Vites = item.Vites;
                ViewBag.Yakit = item.Yakit;
            }
            else if (tip == "Esya")
            {
                ViewBag.Kategori = item.Kategori;
                ViewBag.Durum = item.Durum;
            }
            else if (tip == "Esya")
            {
                ViewBag.Kategori = item.Kategori;
                ViewBag.Durum = item.Durum;
            }
        }

        public async Task<IActionResult> Arama(string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return View(new SearchViewModel());
            var query = q.ToLower();
            var res = new SearchViewModel { Query = q };

            res.Evler = await _context.Evler.Where(e => e.Aktif && e.Ad.Contains(query)).ToListAsync();
            res.Vasitalar = await _context.Vasitalar.Where(v => v.Aktif && v.Ad.Contains(query)).ToListAsync();
            res.Esyalar = await _context.Esyalar.Where(e => e.Aktif && e.Ad.Contains(query)).ToListAsync();
            res.IsIlanlari = await _context.IsIlanlari.Where(i => i.Aktif && i.Ad.Contains(query)).ToListAsync();

            return View(res);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}