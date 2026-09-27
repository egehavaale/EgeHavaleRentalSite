namespace EgeHavaleProjeOdevi.Models
{
    public class SepetItem
    {
        public int Id { get; set; } 



        
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int IlanId { get; set; }
        public string IlanTipi { get; set; }
        public string IlanAdi { get; set; }
        public decimal Fiyat { get; set; }
        public string GorselUrl { get; set; }

        public DateTime EklemeTarihi { get; set; } = DateTime.UtcNow;
    }
}