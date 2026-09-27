using EgeHavaleProjeOdevi.Data;
using EgeHavaleProjeOdevi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgeHavaleProjeOdevi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.EvCount = await _context.Evler.CountAsync();
            ViewBag.VasitaCount = await _context.Vasitalar.CountAsync();
            ViewBag.EsyaCount = await _context.Esyalar.CountAsync();
            ViewBag.IsIlaniCount = await _context.IsIlanlari.CountAsync();
            ViewBag.UserCount = await _context.Users.CountAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sil(int id, string tip)
        {
            if (tip == "Ev")
            {
                var item = await _context.Evler.FindAsync(id);
                if (item != null) _context.Evler.Remove(item);
            }
            else if (tip == "Vasita")
            {
                var item = await _context.Vasitalar.FindAsync(id);
                if (item != null) _context.Vasitalar.Remove(item);
            }
            else if (tip == "Esya")
            {
                var item = await _context.Esyalar.FindAsync(id);
                if (item != null) _context.Esyalar.Remove(item);
            }
            else if (tip == "IsIlani")
            {
                var item = await _context.IsIlanlari.FindAsync(id);
                if (item != null) _context.IsIlanlari.Remove(item);
            }

            var sepetReferanslari = _context.SepetItems.Where(s => s.IlanId == id && s.IlanTipi == tip);
            _context.SepetItems.RemoveRange(sepetReferanslari);

            await _context.SaveChangesAsync();

            return RedirectToAction(tip == "IsIlani" ? "IsIlanlari" : tip + "ler");
        }

        public async Task<IActionResult> Evler() => View(await _context.Evler.ToListAsync());

        public IActionResult EvForm(int? id)
        {
            if (id.HasValue)
            {
                var ev = _context.Evler.Find(id.Value);
                if (ev == null) return NotFound();
                return View(ev);
            }
            return View(new Ev());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EvForm(Ev ev, IFormFile? uploadImage)
        {
            if (ModelState.IsValid)
            {
                ev.GorselUrl = await ProcessImageAsync(uploadImage, ev.GorselUrl ?? "");
                if (ev.Id == 0) _context.Evler.Add(ev);
                else _context.Evler.Update(ev);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Evler));
            }
            return View(ev);
        }

        public async Task<IActionResult> Vasitalar() => View(await _context.Vasitalar.ToListAsync());

        public IActionResult VasitaForm(int? id)
        {
            if (id.HasValue)
            {
                var vasita = _context.Vasitalar.Find(id.Value);
                if (vasita == null) return NotFound();
                return View(vasita);
            }
            return View(new Vasita());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VasitaForm(Vasita vasita, IFormFile? uploadImage)
        {
            if (ModelState.IsValid)
            {
                vasita.GorselUrl = await ProcessImageAsync(uploadImage, vasita.GorselUrl ?? "");
                if (vasita.Id == 0) _context.Vasitalar.Add(vasita);
                else _context.Vasitalar.Update(vasita);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Vasitalar));
            }
            return View(vasita);
        }

        public async Task<IActionResult> Esyalar() => View(await _context.Esyalar.ToListAsync());

        public IActionResult EsyaForm(int? id)
        {
            if (id.HasValue)
            {
                var esya = _context.Esyalar.Find(id.Value);
                if (esya == null) return NotFound();
                return View(esya);
            }
            return View(new Esya());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EsyaForm(Esya esya, IFormFile? uploadImage)
        {
            if (ModelState.IsValid)
            {
                esya.GorselUrl = await ProcessImageAsync(uploadImage, esya.GorselUrl ?? "");
                if (esya.Id == 0) _context.Esyalar.Add(esya);
                else _context.Esyalar.Update(esya);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Esyalar));
            }
            return View(esya);
        }

        public async Task<IActionResult> IsIlanlari() => View(await _context.IsIlanlari.ToListAsync());

        public IActionResult IsIlaniForm(int? id)
        {
            if (id.HasValue)
            {
                var isIlani = _context.IsIlanlari.Find(id.Value);
                if (isIlani == null) return NotFound();
                return View(isIlani);
            }
            return View(new IsIlani());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsIlaniForm(IsIlani isIlani, IFormFile? uploadImage)
        {
            if (ModelState.IsValid)
            {
                isIlani.GorselUrl = await ProcessImageAsync(uploadImage, isIlani.GorselUrl ?? "");
                if (isIlani.Id == 0) _context.IsIlanlari.Add(isIlani);
                else _context.IsIlanlari.Update(isIlani);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IsIlanlari));
            }
            return View(isIlani);
        }

        private async Task<string> ProcessImageAsync(IFormFile? imageFile, string existingUrl)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                return "/images/" + uniqueFileName;
            }
            return existingUrl;
        }
    }
}