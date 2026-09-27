using EgeHavaleProjeOdevi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgeHavaleProjeOdevi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ev> Evler { get; set; }
        public DbSet<Vasita> Vasitalar { get; set; }
        public DbSet<Esya> Esyalar { get; set; }
        public DbSet<IsIlani> IsIlanlari { get; set; }
        public DbSet<SepetItem> SepetItems { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Kullanicilar");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("Roller");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("KullaniciRolleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("KullaniciTalepleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("KullaniciGirisleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("RolTalepleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("KullaniciTokenlari");
            builder.Entity<SepetItem>().Property(s => s.Fiyat).HasColumnType("decimal(18,2)");
            builder.Entity<Ev>().Property(e => e.Fiyat).HasColumnType("decimal(18,2)");
            builder.Entity<Vasita>().Property(v => v.Fiyat).HasColumnType("decimal(18,2)");
            builder.Entity<Esya>().Property(e => e.Fiyat).HasColumnType("decimal(18,2)");
        }
    }
}
