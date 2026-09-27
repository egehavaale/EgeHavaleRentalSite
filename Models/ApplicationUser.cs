using Microsoft.AspNetCore.Identity;

namespace EgeHavaleProjeOdevi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AdSoyad { get; set; } = string.Empty;
        public DateTime KayitTarihi { get; set; } = DateTime.UtcNow;

        public virtual ICollection<SepetItem> SepetItems { get; set; } = new List<SepetItem>();
    }
}