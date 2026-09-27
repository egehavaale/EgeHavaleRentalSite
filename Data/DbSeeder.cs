using EgeHavaleProjeOdevi.Data;
using EgeHavaleProjeOdevi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EgeHavaleProjeOdevi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            string[] roles = { "Admin", "Kullanici" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            const string adminEmail = "admin@egehavale.com";
            const string adminPassword = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    AdSoyad = "Sistem Yöneticisi",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            if (!context.Evler.Any())
            {
                context.Evler.AddRange(new[]
                {
                    new Ev { Ad = "Deniz Manzaralı Lüks Villa", Detaylar = "Harika doğa ve deniz manzarası.", Adres = "Çeşme, İzmir", Fiyat = 25000000, GorselUrl = "/images/ev1.png", OdaSayisi = "5+2", Metrekare = 450, Isitma = "Yerden Isıtma", Esyali = true, Kimden = "Sahibinden" },
                    new Ev { Ad = "Bahçeli Müstakil Ev", Detaylar = "Yeşillikler içinde huzurlu.", Adres = "Urla, İzmir", Fiyat = 8500000, GorselUrl = "/images/ev2.png", OdaSayisi = "3+1", Metrekare = 150, Isitma = "Kombi", Esyali = false, Kimden = "Emlak Ofisinden" },
                    new Ev { Ad = "Modern Mimari Lüks Villa", Detaylar = "Yeni nesil akıllı ev sistemi.", Adres = "Bodrum, Muğla", Fiyat = 35000000, GorselUrl = "/images/ev3.png", OdaSayisi = "4+1", Metrekare = 300, Isitma = "Merkezi", Esyali = true, Kimden = "Müteahhitten" },
                    new Ev { Ad = "Şehir Merkezinde Modern Daire", Detaylar = "Ulaşım araçlarına ve AVM'lere yürüme mesafesinde.", Adres = "Şişli, İstanbul", Fiyat = 12000000, GorselUrl = "/images/ev4.png", OdaSayisi = "2+1", Metrekare = 110, Isitma = "Kombi", Esyali = true, Kimden = "Sahibinden" },
                    new Ev { Ad = "Orman İçinde Doğa Evi", Detaylar = "Ahşap tasarım ile doğanın kalbinde.", Adres = "Sapanca, Sakarya", Fiyat = 4500000, GorselUrl = "/images/ev5.png", OdaSayisi = "3+1", Metrekare = 130, Isitma = "Şömine", Esyali = true, Kimden = "Sahibinden" },
                    new Ev { Ad = "Havuzlu Yeni Proje Daire", Detaylar = "Ultra lüks akıllı site içerisinde rezidans.", Adres = "Ataşehir, İstanbul", Fiyat = 18500000, GorselUrl = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&q=80", OdaSayisi = "3+1", Metrekare = 180, Isitma = "Yerden Isıtma", Esyali = false, Kimden = "Müteahhitten" },
                    new Ev { Ad = "Tarihi Taş Ev", Detaylar = "Restorasyonu yeni tamamlanmış, aslına uygun şömineli ev.", Adres = "Alaçatı, İzmir", Fiyat = 22000000, GorselUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800&q=80", OdaSayisi = "4+1", Metrekare = 210, Isitma = "Şömine", Esyali = true, Kimden = "Sahibinden" },
                    new Ev { Ad = "Denize Sıfır Yalı", Detaylar = "Kendine ait iskelesi bulunan yalı dairesi.", Adres = "Sarıyer, İstanbul", Fiyat = 85000000, GorselUrl = "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&q=80", OdaSayisi = "5+1", Metrekare = 320, Isitma = "Kombi", Esyali = true, Kimden = "Emlak Ofisinden" }
                });
            }

            if (!context.Vasitalar.Any())
            {
                context.Vasitalar.AddRange(new[]
                {
                    new Vasita { Ad = "2020 Model Temiz Sedan", Detaylar = "Hatasız boyasız ilk sahibinden.", Adres = "Kadıköy, İstanbul", Fiyat = 1200000, GorselUrl = "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?w=800&q=80", ModelYili = 2020, Kilometre = 45000, Vites = "Otomatik", Yakit = "Benzin", Kimden = "Sahibinden" },
                    new Vasita { Ad = "A++ SUV Konforu", Detaylar = "Uzun yol aracı, ful paket.", Adres = "Çankaya, Ankara", Fiyat = 2500000, GorselUrl = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800&q=80", ModelYili = 2023, Kilometre = 12000, Vites = "Otomatik", Yakit = "Dizel", Kimden = "Galeriden" },
                    new Vasita { Ad = "Spor Coupe", Detaylar = "Kırmızı şeytan, garaj arabası.", Adres = "Bornova, İzmir", Fiyat = 4800000, GorselUrl = "https://images.unsplash.com/photo-1583121274602-3e2820c69888?w=800&q=80", ModelYili = 2022, Kilometre = 8000, Vites = "Otomatik", Yakit = "Benzin", Kimden = "Sahibinden" },
                    new Vasita { Ad = "Elektrikli Gelecek", Detaylar = "Pil sağlığı %100, garantisi devam ediyor.", Adres = "Nilüfer, Bursa", Fiyat = 2100000, GorselUrl = "https://images.unsplash.com/photo-1560958089-b8a1929cea89?w=800&q=80", ModelYili = 2024, Kilometre = 5000, Vites = "Otomatik", Yakit = "Elektrik", Kimden = "Sahibinden" }
                });
            }

            // --- Eşyalar ---
            if (!context.Esyalar.Any())
            {
                context.Esyalar.AddRange(new[]
                {
                    new Esya { Ad = "Az Kullanılmış Playstation 5", Detaylar = "Çizik bile yok, kutusuyla verilecek.", Adres = "Buca, İzmir", Fiyat = 18000, GorselUrl = "https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=800&q=80", Kategori = "Elektronik", Durum = "İkinci El", Kimden = "Sahibinden" },
                    new Esya { Ad = "Antika Ahşap Yemek Masası", Detaylar = "El oyması özel üretim.", Adres = "Beyoğlu, İstanbul", Fiyat = 12000, GorselUrl = "https://images.unsplash.com/photo-1533090481720-856c6e3c1fdc?w=800&q=80", Kategori = "Mobilya", Durum = "İkinci El", Kimden = "Mağazadan" },
                    new Esya { Ad = "Mirrorless Analog Kamera", Detaylar = "Koleksiyonerden lensleriyle beraber.", Adres = "Konak, İzmir", Fiyat = 25000, GorselUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=800&q=80", Kategori = "Kamera", Durum = "İkinci El", Kimden = "Sahibinden" },
                    new Esya { Ad = "Oyuncu Monitörü 144hz", Detaylar = "Hiç açılmamış, jelatini üzerinde.", Adres = "Odunpazarı, Eskişehir", Fiyat = 6500, GorselUrl = "https://images.unsplash.com/photo-1552831388-6a0b35ece736?w=800&q=80", Kategori = "Bilgisayar", Durum = "Sıfır", Kimden = "Mağazadan" }
                });
            }

            if (!context.IsIlanlari.Any())
            {
                context.IsIlanlari.AddRange(new[]
                {
                    new IsIlani { Ad = "Yazılım Uzmanı Aranıyor", Sirket = "TechGlobal A.Ş.", Adres = "Levent, İstanbul", Detaylar = "En az 3 yıl .NET tecrübesine sahip takım arkadaşı arıyoruz.", Pozisyon = "Senior Developer", CalismaTipi = "Tam Zamanlı", GorselUrl = "https://images.unsplash.com/photo-1498050108023-c5249f4df085?w=800&q=80" },
                    new IsIlani { Ad = "Kurumsal Satış Temsilcisi", Sirket = "EmlakPlus", Adres = "Alsancak, İzmir", Detaylar = "İkna kabiliyeti yüksek, müşteri portföyüne sahip satış temsilcisi.", Pozisyon = "Satış Temsilcisi", CalismaTipi = "Tam Zamanlı", GorselUrl = "https://images.unsplash.com/photo-1560250097-0b93528c311a?w=800&q=80" },
                    new IsIlani { Ad = "Grafik Tasarımcı", Sirket = "Kreatif Ajans", Adres = "Beşiktaş, İstanbul", Detaylar = "Adobe Suite hakim, modern arayüz tasarlayabilecek vizyoner kreatif.", Pozisyon = "UI/UX Designer", CalismaTipi = "Yarı Zamanlı", GorselUrl = "https://images.unsplash.com/photo-1559028012-481c04fa702d?width=800" }
                });
            }

            await context.SaveChangesAsync();
        }
    }
}