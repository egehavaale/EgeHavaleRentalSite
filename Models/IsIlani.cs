namespace EgeHavaleProjeOdevi.Models
{
    public class IsIlani
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Sirket { get; set; } = string.Empty;
        public string Detaylar { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string Pozisyon { get; set; } = string.Empty;
        public string CalismaTipi { get; set; } = string.Empty;
        public string GorselUrl { get; set; } = string.Empty;
        public DateTime EklenmeTarihi { get; set; } = DateTime.UtcNow;
        public bool Aktif { get; set; } = true;
    }
}
