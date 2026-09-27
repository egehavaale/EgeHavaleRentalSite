namespace EgeHavaleProjeOdevi.Models
{
    public class Vasita
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Detaylar { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public decimal Fiyat { get; set; }
        public string GorselUrl { get; set; } = string.Empty;
        public int ModelYili { get; set; }
        public int Kilometre { get; set; }
        public string Vites { get; set; } = string.Empty;
        public string Yakit { get; set; } = string.Empty;
        public string Kimden { get; set; } = string.Empty;
        public DateTime EklenmeTarihi { get; set; } = DateTime.UtcNow;
        public bool Aktif { get; set; } = true;
    }
}
