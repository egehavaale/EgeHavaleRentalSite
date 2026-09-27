//using Microsoft.AspNetCore.Mvc;
//using EgeHavaleProjeOdevi.Models;
//using EgeHavaleProjeOdevi.Data;
//using Microsoft.AspNetCore.Identity;
//using System.Linq;
//using System.Security.Claims;

//namespace EgeHavaleProjeOdevi.Controllers
//{
//    public class SepetController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<ApplicationUser> _userManager;

//        public SepetController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // Display items belonging only to the logged-in user
//        public IActionResult Index()
//        {
//            var userId = _userManager.GetUserId(User);
//            var sepetim = _context.SepetItems
//                                  .Where(s => s.UserId == userId)
//                                  .ToList();
//            return View(sepetim);
//        }

//        [HttpPost]
//        public IActionResult Ekle(int id, string tip)
//        {
//            var userId = _userManager.GetUserId(User);
//            if (userId == null) return RedirectToAction("Login", "Account");

//            SepetItem yeniItem = null;

//            if (tip == "Vasita")
//            {
//                var v = _context.Vasitalar.FirstOrDefault(x => x.Id == id);
//                if (v != null)
//                {
//                    yeniItem = new SepetItem { IlanId = v.Id, IlanAdi = v.Ad, Fiyat = v.Fiyat, GorselUrl = v.GorselUrl, IlanTipi = tip, UserId = userId };
//                }
//            }
//            else if (tip == "Esya")
//            {
//                var e = _context.Esyalar.FirstOrDefault(x => x.Id == id);
//                if (e != null)
//                {
//                    yeniItem = new SepetItem { IlanId = e.Id, IlanAdi = e.Ad, Fiyat = e.Fiyat, GorselUrl = e.GorselUrl, IlanTipi = tip, UserId = userId };
//                }
//            }

//            if (yeniItem != null)
//            {
//                _context.SepetItems.Add(yeniItem);
//                _context.SaveChanges();
//            }

//            return RedirectToAction("Index");
//        }

//        [HttpPost]
//        public IActionResult Cikar(int id)
//        {
//            var item = _context.SepetItems.Find(id);
//            if (item != null)
//            {
//                _context.SepetItems.Remove(item);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}

using EgeHavaleProjeOdevi.Data;
using EgeHavaleProjeOdevi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EgeHavaleProjeOdevi.Controllers
{
    [Authorize] 
    public class SepetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SepetController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var items = await _context.SepetItems
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(int id, string tip)
        {
            var userId = _userManager.GetUserId(User);

            var existing = await _context.SepetItems
                .AnyAsync(s => s.IlanId == id && s.IlanTipi == tip && s.UserId == userId);

            if (existing) return RedirectToAction("Index");

            SepetItem itemToAdd = null;

            // Fetch actual data from DB based on the type to prevent price/name spoofing
            if (tip == "Emlak")
            {
                var ev = await _context.Evler.FindAsync(id);
                if (ev != null) itemToAdd = CreateSepetItem(ev.Id, "Emlak", ev.Ad, ev.Fiyat, ev.GorselUrl, userId);
            }
            else if (tip == "Vasita")
            {
                var v = await _context.Vasitalar.FindAsync(id);
                if (v != null) itemToAdd = CreateSepetItem(v.Id, "Vasita", v.Ad, v.Fiyat, v.GorselUrl, userId);
            }
            else if (tip == "Esya")
            {
                var e = await _context.Esyalar.FindAsync(id);
                if (e != null) itemToAdd = CreateSepetItem(e.Id, "Esya", e.Ad, e.Fiyat, e.GorselUrl, userId);
            }

            if (itemToAdd != null)
            {
                _context.SepetItems.Add(itemToAdd);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Cikar(int id)
        {
            var item = await _context.SepetItems.FindAsync(id);
            if (item != null)
            {
                _context.SepetItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        private SepetItem CreateSepetItem(int id, string tip, string ad, decimal fiyat, string url, string uId)
        {
            return new SepetItem
            {
                IlanId = id,
                IlanTipi = tip,
                IlanAdi = ad,
                Fiyat = fiyat,
                GorselUrl = url,
                UserId = uId
            };
        }
    }
}