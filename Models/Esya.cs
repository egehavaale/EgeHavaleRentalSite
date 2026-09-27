namespace EgeHavaleProjeOdevi.Models
{
    public class Esya
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Detaylar { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public decimal Fiyat { get; set; }
        public string GorselUrl { get; set; } = string.Empty;
        public string Durum { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public string Kimden { get; set; } = string.Empty;
        public DateTime EklenmeTarihi { get; set; } = DateTime.UtcNow;
        public bool Aktif { get; set; } = true;
    }
}
